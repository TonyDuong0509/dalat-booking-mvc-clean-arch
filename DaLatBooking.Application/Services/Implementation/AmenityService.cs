using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Application.Services.Interface;
using DaLatBooking.Domain.Entities;

namespace DaLatBooking.Application.Services.Implementation
{
    public class AmenityService : IAmenityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AmenityService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void CreateAmenity(Amenity amenity)
        {
            _unitOfWork.Amenity.Add(amenity);
            _unitOfWork.Save();
        }

        public bool DeleteAmenity(int id)
        {
            try
            {
                Amenity? modelFromDb = GetAmenityById(id);
                if (modelFromDb is not null)
                {
                    _unitOfWork.Amenity.Delete(modelFromDb);
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

        public IEnumerable<Amenity> GetAllAmenities()
        {
            return _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
        }

        public Amenity GetAmenityById(int id)
        {
            return _unitOfWork.Amenity.Get(x => x.Id == id);
        }

        public void UpdateAmenity(Amenity amenity)
        {
            _unitOfWork.Amenity.Update(amenity);
            _unitOfWork.Save();
        }
    }
}
