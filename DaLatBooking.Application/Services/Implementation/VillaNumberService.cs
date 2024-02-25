using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Application.Services.Interface;
using DaLatBooking.Domain.Entities;
using Microsoft.AspNetCore.Hosting;

namespace DaLatBooking.Application.Services.Implementation
{
    public class VillaNumberService : IVillaNumberService
    {
        private readonly IUnitOfWork _unitOfWork;
        public VillaNumberService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool CheckVillaNumberExists(int villa_Number)
        {
            return _unitOfWork.VillaNumber.Any(x => x.Villa_Number == villa_Number);
        }

        public void CreateVillaNumber(VillaNumber villaNumber)
        {
            _unitOfWork.VillaNumber.Add(villaNumber);
            _unitOfWork.Save();
        }

        public bool DeleteVillaNumber(int id)
        {
            try
            {
                VillaNumber? objFromDb = _unitOfWork.VillaNumber.Get(x => x.Villa_Number == id);
                if (objFromDb is not null)
                {
                    _unitOfWork.VillaNumber.Delete(objFromDb);
                    _unitOfWork.Save();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<VillaNumber> GetAllVillaNumbers()
        {
            return _unitOfWork.VillaNumber.GetAll(includeProperties: "Villa");
        }

        public VillaNumber GetVillaNumberById(int id)
        {
            return _unitOfWork.VillaNumber.Get(x => x.Villa_Number == id, includeProperties: "Villa");
        }

        public void UpdateVillaNumber(VillaNumber villaNumber)
        {
            _unitOfWork.VillaNumber.Update(villaNumber);
            _unitOfWork.Save();
        }
    }
}
