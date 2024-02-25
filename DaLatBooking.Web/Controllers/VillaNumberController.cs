using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Application.Services.Interface;
using DaLatBooking.Domain.Entities;
using DaLatBooking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DaLatBooking.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        private readonly IVillaService _villaService;
        public VillaNumberController(IVillaNumberService villaNumberService, IVillaService villaService)
        {
            this._villaNumberService = villaNumberService;
            this._villaService = villaService;
        }

        public IActionResult Index()
        {
            var villaNumbers = _villaNumberService.GetAllVillaNumbers();
            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
               VillaList = _villaService.GetAllVillas().Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    })
            };
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM model)
        {
            bool roomNumberExist = _villaNumberService.CheckVillaNumberExists(model.VillaNumber.Villa_Number);

            if (ModelState.IsValid && !roomNumberExist) 
            {
                _villaNumberService.CreateVillaNumber(model.VillaNumber);
                TempData["success"] = "Số phòng này đã thêm thành công !";
                return RedirectToAction(nameof(Index));
            }

            if (roomNumberExist)
            {
                TempData["error"] = "Số phòng của loại phòng này đã tồn tại";
            }
            model.VillaList = _villaService.GetAllVillas().Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    });

            return View(model);
        }

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _villaService.GetAllVillas().Select(x => new SelectListItem
                   {
                       Text = x.Name,
                       Value = x.Id.ToString()
                   }),
                VillaNumber = _villaNumberService.GetVillaNumberById(villaNumberId)
            };

            if (villaNumberVM == null) return RedirectToAction("Error", "Home");
            
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {
            if (ModelState.IsValid)
            {
                _villaNumberService.UpdateVillaNumber(villaNumberVM.VillaNumber);
                TempData["success"] = "Số phòng này đã chỉnh sửa thành công !";
                return RedirectToAction(nameof(Index));
            }
            villaNumberVM.VillaList =_villaService.GetAllVillas().Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    });

            return View(villaNumberVM);
        }

        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _villaService.GetAllVillas().Select(x => new SelectListItem
                  {
                      Text = x.Name,
                      Value = x.Id.ToString()
                  }),
                VillaNumber = _villaNumberService.GetVillaNumberById(villaNumberId)
            };

            if (villaNumberVM == null) return RedirectToAction("Error", "Home");

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {
            VillaNumber? modelFromDb = _villaNumberService.GetVillaNumberById(villaNumberVM.VillaNumber.Villa_Number);
            if (modelFromDb is not null)
            {
                _villaNumberService.DeleteVillaNumber(modelFromDb.Villa_Number);
                TempData["success"] = "Loại phòng này đã được xoá thành công !";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Không thể xoá phòng này. Vui lòng kiểm tra lại !";
            return View();
        }
    }
}
