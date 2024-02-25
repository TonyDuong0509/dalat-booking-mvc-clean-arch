using DaLatBooking.Application.Common.Utility;
using DaLatBooking.Application.Services.Interface;
using DaLatBooking.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DaLatBooking.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class AmenityController : Controller
    {
        private readonly IAmenityService _amenityService;
        private readonly IVillaService _villaService;
        public AmenityController(IAmenityService amenityService, IVillaService villaService)
        {
            this._amenityService = amenityService;
            this._villaService = villaService;
        }

        public IActionResult Index()
        {
            var amenities = _amenityService.GetAllAmenities();
            return View(amenities);
        }

        public IActionResult Create()
        {
            AmenityVM amenitieVM = new()
            {
                VillaList = _villaService.GetAllVillas().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(amenitieVM);
        }

        [HttpPost]
        public IActionResult Create(AmenityVM model)
        {
            if (ModelState.IsValid)
            {
                _amenityService.CreateAmenity(model.Amenity);
                TempData["success"] = "Dịch vụ tiện nghi này đã thêm thành công !";
                return RedirectToAction(nameof(Index));
            }
            model.VillaList = _villaService.GetAllVillas().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(model);
        }

        public IActionResult Update(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _villaService.GetAllVillas().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Amenity = _amenityService.GetAmenityById(amenityId)
            };

            if (amenityVM == null) return RedirectToAction("Error", "Home");

            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Update(AmenityVM amenityVM)
        {
            if (ModelState.IsValid)
            {
                _amenityService.UpdateAmenity(amenityVM.Amenity);
                TempData["success"] = "Dịch vụ tiện nghi này đã chỉnh sửa thành công !";
                return RedirectToAction(nameof(Index));
            }
            amenityVM.VillaList = _villaService.GetAllVillas().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(amenityVM);
        }

        public IActionResult Delete(int amenityId)
        {
            AmenityVM amenitieVM = new()
            {
                VillaList = _villaService.GetAllVillas().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Amenity = _amenityService.GetAmenityById(amenityId)
            };

            if (amenitieVM == null) return RedirectToAction("Error", "Home");

            return View(amenitieVM);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM amenityVM)
        {
            var deleted = _amenityService.DeleteAmenity(amenityVM.Amenity.Id);
            if (deleted)
            {
                TempData["success"] = "Dịch vụ tiện nghi này đã được xoá thành công !";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Không thể xoá dịch vụ tiện nghi này. Vui lòng kiểm tra lại !";
            }
            return View();
        }
    }
}
