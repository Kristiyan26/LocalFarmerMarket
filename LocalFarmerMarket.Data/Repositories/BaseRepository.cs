using LocalFarmerMarket.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LocalFarmerMarket.Data.Repositories
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        protected DbContext Context { get; set; }
        protected DbSet<T> Items { get; set; }

        public BaseRepository(LocalFarmerMarketDbContext context)
        {
            Context = context;
            Items = Context.Set<T>();
        }


        public void Delete(T item)
        {
            Items.Remove(item);
            Context.SaveChanges();
        }

        public void Save(T item)
        {
            if (item.Id > 0)
            {
                Items.Update(item);
            }
            else
            {
                Items.Add(item);
            }

            Context.SaveChanges();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = Items;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.FirstOrDefault();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null,
                              Expression<Func<T, bool>> orderBy = null,
                              int page = 1,
                              int itemsPerPage = Int32.MaxValue)
        {
            IQueryable<T> query = Items;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            return query.Skip(itemsPerPage * (page - 1)).Take(itemsPerPage).ToList();
        }
    }
}
