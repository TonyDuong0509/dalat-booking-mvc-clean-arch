using DaLatBooking.Application.Services.Interface;
using DaLatBooking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaLatBooking.Web.Controllers
{
    [Authorize]
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        public VillaController(IVillaService villaService)
        {
            this._villaService = villaService;
        }

        public IActionResult Index()
        {
            var villas = _villaService.GetAllVillas();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa model)
        {
            if (model.Description == model.Name)
            {
                ModelState.AddModelError("description", "Mô tả phòng không thể chỉ chứa giống tên phòng !");
            }
            if (ModelState.IsValid) 
            {
               _villaService.CreateVilla(model);
                TempData["success"] = "Loại phòng này đã thêm thành công !";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Update(int villaId)
        {
            Villa? model = _villaService.GetVillaById(villaId);

            if (model == null) return RedirectToAction("Error", "Home");
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Villa model)
        {
            if (ModelState.IsValid) 
            {
                _villaService.UpdateVilla(model);
                TempData["success"] = "Loại phòng này đã chỉnh sửa thành công !";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int villaId)
        {
            Villa? model = _villaService.GetVillaById(villaId);

            if (model is null) return RedirectToAction("Error", "Home");

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Villa model)
        {
            bool deleted = _villaService.DeleteVilla(model.Id);
            if (deleted)
            {
                TempData["success"] = "Loại phòng này đã được xoá thành công !";
                return RedirectToAction(nameof(Index));
            }
            else 
            {
            TempData["error"] = "Không thể xoá phòng này. Vui lòng kiểm tra lại !";

            }
            return View();
        }
    }
}
