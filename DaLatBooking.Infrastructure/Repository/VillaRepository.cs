using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Domain.Entities;
using DaLatBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DaLatBooking.Infrastructure.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _context;
        public VillaRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }


        public void Update(Villa entity)
        {
            _context.Villas.Update(entity);
        }
    }
}
