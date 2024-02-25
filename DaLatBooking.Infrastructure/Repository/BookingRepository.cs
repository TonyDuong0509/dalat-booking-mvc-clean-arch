using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Application.Common.Utility;
using DaLatBooking.Domain.Entities;
using DaLatBooking.Infrastructure.Data;

namespace DaLatBooking.Infrastructure.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }


        public void Update(Booking entity)
        {
            _context.Bookings.Update(entity);
        }
    }
}
