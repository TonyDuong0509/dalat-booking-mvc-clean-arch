using DaLatBooking.Domain.Entities;

namespace DaLatBooking.Application.Common.Interfaces
{
    public interface IAmenityRepository : IRepository<Amenity>
    {
        void Update(Amenity entity);
    }
}
