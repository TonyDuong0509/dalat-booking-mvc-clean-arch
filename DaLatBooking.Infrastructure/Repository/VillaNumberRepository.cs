using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Domain.Entities;
using DaLatBooking.Infrastructure.Data;

namespace DaLatBooking.Infrastructure.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _context;
        public VillaNumberRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public void Update(VillaNumber entity)
        {
           _context.VillaNumbers.Update(entity);
        }
    }
}
