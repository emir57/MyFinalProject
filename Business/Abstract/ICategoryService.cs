using Core.Utilities.Results;
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
        Task<IDataResult<List<Category>>> GetAll();
        Task<IDataResult<Category>> GetById(int categoryId);
        Task Add(Category entity);
        Task Update(Category entity);
        Task Delete(Category entity);
    }
}
