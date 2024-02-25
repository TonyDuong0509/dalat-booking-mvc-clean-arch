using DaLatBooking.Web.ViewModels;

namespace DaLatBooking.Application.Services.Interface
{
    public interface IDashboardService
    {
        Task<RadialBarChartDto> GetTotalBookingRadialChartData();
        Task<RadialBarChartDto> GetRegisterUserChartData();
        Task<RadialBarChartDto> GetRevenueChartData();
    }
}
