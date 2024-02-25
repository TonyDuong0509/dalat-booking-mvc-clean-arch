using DaLatBooking.Application.Common.Utility;
using DaLatBooking.Application.Services.Interface;
using DaLatBooking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace DaLatBooking.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IVillaService _villaService;
        private readonly IVillaNumberService _villaNumberService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPaymentService _paymentService;
        private readonly UserManager<User> _userManager;

        public BookingController(IBookingService bookingService,
                IVillaService villaService,
                IVillaNumberService villaNumberService,
                IWebHostEnvironment webHostEnvironment,
                UserManager<User> userManager,
                IPaymentService paymentService)
        {
            this._bookingService = bookingService;
            this._villaService = villaService;
            this._villaNumberService = villaNumberService;
            this._webHostEnvironment = webHostEnvironment;
            this._userManager = userManager;
            this._paymentService = paymentService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            User user = _userManager.FindByIdAsync(userId).GetAwaiter().GetResult();

            Booking booking = new()
            {
                VillaId = villaId,
                Villa = _villaService.GetVillaById(villaId),
                CheckInDate = checkInDate,
                Nights = nights,
                CheckOutDate = checkInDate.AddDays(nights),
                UserId = userId,
                Name = user.Name,
                Phone = user.PhoneNumber,
                Email = user.Email
            };
            booking.TotalCost = booking.Villa.Price * nights;
            return View(booking);
        }

        [Authorize]
        [HttpPost]
        public IActionResult FinalizeBooking(Booking booking)
        {
            var villa = _villaService.GetVillaById(booking.VillaId);
            booking.TotalCost = villa.Price * booking.Nights;
            booking.Status = SD.StatusPending;
            booking.BookingDate = DateTime.Now;

            var roomAvailable = _villaService.IsVillaAvailableByDate(villa.Id, booking.Nights, booking.CheckInDate);

            if (!roomAvailable)
            {
                TempData["Error"] = "Đã hết phòng này, phiền bạn xem ngày khác hoặc phòng khác !";
                // no rooms available
                return RedirectToAction(nameof(FinalizeBooking), new
                {
                    villaId = booking.VillaId,
                    checkInDate = booking.CheckInDate,
                    nights = booking.Nights,
                });
            }

            _bookingService.CreateBooking(booking);

            var domain = Request.Scheme + "://" + Request.Host.Value + "/";

            var options = _paymentService.CreateStripeSessionOptions(booking, villa, domain);

            var session = _paymentService.CreateStripeSession(options);

            _bookingService.UpdateStripePaymentID(booking.Id, session.Id, session.PaymentIntentId);
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [Authorize]
        public IActionResult BookingConfirmation(int bookingId)
        {
            Booking bookingFromDb = _bookingService.GetBookingId(bookingId);

            if (bookingFromDb.Status == SD.StatusPending)
            {
                //this is a pending order, we need to confirm if payment was successful

                var service = new SessionService();
                Session session = service.Get(bookingFromDb.StripeSessionId);

                if (session.PaymentStatus == "paid")
                {
                    _bookingService.UpdateStatus(bookingFromDb.Id, SD.StatusApproved, 0);
                    _bookingService.UpdateStripePaymentID(bookingFromDb.Id, session.Id, session.PaymentIntentId);
                }
            }

            return View(bookingId);
        }

        [Authorize]
        public IActionResult BookingDetails(int bookingId)
        {
            Booking bookingFromDb = _bookingService.GetBookingId(bookingId);

            if (bookingFromDb.VillaNumber == 0 && bookingFromDb.Status == SD.StatusApproved)
            {
                var availableVillaNumber = AssignAvailableVillaNumberByVilla(bookingFromDb.VillaId);

                bookingFromDb.VillaNumbers =_villaNumberService.GetAllVillaNumbers().Where
                    (x => x.VillaId == bookingFromDb.VillaId
                    && availableVillaNumber.Any(y => y == x.Villa_Number)).ToList();
            }

            return View(bookingFromDb);
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult CheckIn(Booking booking)
        {
            _bookingService.UpdateStatus(booking.Id, SD.StatusCheckedIn, booking.VillaNumber);
            TempData["Success"] = "Check in thành công !";
            return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult CheckOut(Booking booking)
        {
            _bookingService.UpdateStatus(booking.Id, SD.StatusCompleted, booking.VillaNumber);
            TempData["Success"] = "Check out thành công !";
            return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult CancelBooking(Booking booking)
        {
           _bookingService.UpdateStatus(booking.Id, SD.StatusCancelled, 0);
            TempData["Success"] = "Huỷ booking thành công !";
            return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
        }

        private List<int> AssignAvailableVillaNumberByVilla(int villaId)
        {
            List<int> availableVillaNumbers = new();

            var villaNumbers = _villaNumberService.GetAllVillaNumbers().Where(x => x.VillaId == villaId);

            var checkedInVilla = _bookingService.GetCheckedInVillaNumbers(villaId);

            foreach (var villaNumber in villaNumbers)
            {
                if (!checkedInVilla.Contains(villaNumber.Villa_Number))
                {
                    availableVillaNumbers.Add(villaNumber.Villa_Number);
                }
            }
            return availableVillaNumbers;
        }

        #region API Calls
        [HttpGet]
        [Authorize]
        public IActionResult GetAll(string status)
        {
            IEnumerable<Booking> objBookings;
            string userId = "";
            if (string.IsNullOrEmpty(userId))
            {
                status = "";
            }
            if (!User.IsInRole(SD.Role_Admin))
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            objBookings = _bookingService.GetAllBookings(userId, status);

            return Json(new { data = objBookings });
        }
        #endregion
    }
}

