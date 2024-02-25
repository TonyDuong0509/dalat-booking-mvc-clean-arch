using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Application.Common.Utility;
using DaLatBooking.Application.Services.Interface;
using DaLatBooking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DaLatBooking.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            this._dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTotalBookingRadialChartData()
        {
            return Json(await _dashboardService.GetTotalBookingRadialChartData());
        }

        public async Task<IActionResult> GetRegisterUserChartData()
        {
            return Json(await _dashboardService.GetRegisterUserChartData());
        }

        public async Task<IActionResult> GetRevenueChartData()
        {
            return Json(await _dashboardService.GetRevenueChartData());
        }
    }
}
