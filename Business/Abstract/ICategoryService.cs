using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll(Expression<Func<Category, bool>> filter = null);
        Task<Category> Get(Expression<Func<Category, bool>> filter);
        Task<Category> GetById(int categoryId);
        Task Add(Category entity);
        Task Update(Category entity);
        Task Delete(Category entity);
    }
}
