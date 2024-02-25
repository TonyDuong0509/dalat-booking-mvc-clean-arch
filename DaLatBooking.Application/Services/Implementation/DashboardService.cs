using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Application.Common.Utility;
using DaLatBooking.Application.Services.Interface;
using DaLatBooking.Web.ViewModels;

namespace DaLatBooking.Application.Services.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        static int previousMonth = DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1;
        readonly DateTime previousMonthStartDate = new(DateTime.Now.Year, previousMonth, 1);
        readonly DateTime currentMonthStartDate = new(DateTime.Now.Year, DateTime.Now.Month, 1);

        public DashboardService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<RadialBarChartDto> GetRegisterUserChartData()
        {
            var totalUsers = _unitOfWork.User.GetAll();

            var countByPreviousMonth = totalUsers.Count(x => x.CreatedAt >= previousMonthStartDate &&
            x.CreatedAt <= currentMonthStartDate);

            var countByCurrentMonth = totalUsers.Count(x => x.CreatedAt >= currentMonthStartDate &&
            x.CreatedAt <= DateTime.Now);

            return SD.GetRadialCartDataModel(totalUsers.Count(), countByCurrentMonth, countByPreviousMonth);
        }

        public async Task<RadialBarChartDto> GetRevenueChartData()
        {
            var totalBookings = _unitOfWork.Booking.GetAll(x => x.Status != SD.StatusPending
           || x.Status == SD.StatusCancelled);

            var totalRevenue = Convert.ToInt32(totalBookings.Sum(x => x.TotalCost));

            var countByPreviousMonth = totalBookings.Where(x => x.BookingDate >= previousMonthStartDate &&
               x.BookingDate <= currentMonthStartDate).Sum(x => x.TotalCost);

            var countByCurrentMonth = totalBookings.Where(x => x.BookingDate >= currentMonthStartDate &&
            x.BookingDate <= DateTime.Now).Sum(x => x.TotalCost);

            return SD.GetRadialCartDataModel(totalRevenue, countByCurrentMonth, countByPreviousMonth);
        }

        public async Task<RadialBarChartDto> GetTotalBookingRadialChartData()
        {
            var totalBookings = _unitOfWork.Booking.GetAll(x => x.Status != SD.StatusPending
            || x.Status == SD.StatusCancelled);

            var countByPreviousMonth = totalBookings.Count(x => x.BookingDate >= previousMonthStartDate &&
            x.BookingDate <= currentMonthStartDate);

            var countByCurrentMonth = totalBookings.Count(x => x.BookingDate >= currentMonthStartDate &&
            x.BookingDate <= DateTime.Now);

            return SD.GetRadialCartDataModel(totalBookings.Count(), countByCurrentMonth, countByPreviousMonth);
        }
    }
}
