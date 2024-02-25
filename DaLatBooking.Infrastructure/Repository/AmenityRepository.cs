using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Domain.Entities;
using DaLatBooking.Infrastructure.Data;

namespace DaLatBooking.Infrastructure.Repository
{
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDbContext _context;
        public AmenityRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }


        public void Update(Amenity entity)
        {
            _context.Amenities.Update(entity);
        }
    }
}
