using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        public async Task Add(Product entity)
        {
            using (var context = new NorthwindContext())
            {
                context.Entry(entity).State = EntityState.Added;
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(Product entity)
        {
            using (var context = new NorthwindContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<Product> Get(Expression<Func<Product, bool>> filter)
        {
            using (var context = new NorthwindContext())
            {
                return await context.Set<Product>().SingleOrDefaultAsync(filter);
            }
        }

        public async Task<List<Product>> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (var context = new NorthwindContext())
            {
                return filter == null ?
                    await context.Set<Product>().ToListAsync() :
                    await context.Set<Product>().Where(filter).ToListAsync();
            }
        }

        public async Task Update(Product entity)
        {
            using (var context = new NorthwindContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
