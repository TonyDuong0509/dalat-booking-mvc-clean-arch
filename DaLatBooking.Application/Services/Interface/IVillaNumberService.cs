using DaLatBooking.Domain.Entities;

namespace DaLatBooking.Application.Services.Interface
{
    public interface IVillaNumberService
    {
        IEnumerable<VillaNumber> GetAllVillaNumbers();
        VillaNumber GetVillaNumberById(int id);
        void CreateVillaNumber(VillaNumber villa);
        void UpdateVillaNumber(VillaNumber villa);
        bool DeleteVillaNumber(int id);
        bool CheckVillaNumberExists(int villa_Number);
    }
}
