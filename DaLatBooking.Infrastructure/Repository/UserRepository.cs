using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Domain.Entities;
using DaLatBooking.Infrastructure.Data;

namespace DaLatBooking.Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
