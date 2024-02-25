using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DaLatBooking.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> contextSet;
        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            this.contextSet = _context.Set<T>();
        }    

        public void Add(T entity)
        {
            contextSet.Add(entity);
        }

        public bool Any(Expression<Func<T, bool>> filter)
        {
            return contextSet.Any(filter);
        }

        public void Delete(T entity)
        {
            contextSet.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, 
                string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = contextSet;
            }
            else
            {
                query = contextSet.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                // Villa, VillaNumber -- case sensitive
                foreach (var includeProp in includeProperties
                                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, 
                string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = contextSet;
            }
            else
            {
                query = contextSet.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }

            return query.ToList();
        }
    }
}
