using DaLatBooking.Domain.Entities;
using System.Linq.Expressions;

namespace DaLatBooking.Application.Common.Interfaces
{
    public interface IVillaRepository : IRepository<Villa>
    {
        void Update(Villa entity);
    }
}
