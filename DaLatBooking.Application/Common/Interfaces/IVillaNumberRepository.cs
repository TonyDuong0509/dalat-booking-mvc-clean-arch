using DaLatBooking.Domain.Entities;
using System.Linq.Expressions;

namespace DaLatBooking.Application.Common.Interfaces
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        void Update(VillaNumber entity);
    }
}
