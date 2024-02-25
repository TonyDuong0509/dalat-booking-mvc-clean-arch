using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Application.Common.Utility;
using DaLatBooking.Application.Services.Interface;
using DaLatBooking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DaLatBooking.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaService _villaService;
        public HomeController(IVillaService villaService, IWebHostEnvironment webHostEnvironment)
        {
            this._villaService = villaService;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                VillaList = _villaService.GetAllVillas(),
                Nights = 1,
                CheckInDate = DateOnly.FromDateTime(DateTime.Now)
            };
            return View(homeVM);
        }

        [HttpPost]
        public IActionResult GetVillasByDate(int nights, DateOnly checkInDate)
        {
            HomeVM homeVM = new()
            {
                CheckInDate = checkInDate,
                VillaList = _villaService.GetVillasAvailabilityByDate(nights, checkInDate),
                Nights = nights
            };

            return PartialView("_VillaList", homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
