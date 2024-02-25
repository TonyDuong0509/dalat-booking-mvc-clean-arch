using DaLatBooking.Domain.Entities;

namespace DaLatBooking.Application.Common.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void Update(Booking entity);
    }
}
