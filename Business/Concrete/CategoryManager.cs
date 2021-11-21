using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task Add(Category entity)
        {
            await _categoryDal.Add(entity);
        }

        public async Task Delete(Category entity)
        {
            await _categoryDal.Delete(entity);
        }

        public async Task<Category> Get(Expression<Func<Category, bool>> filter)
        {
            return await _categoryDal.Get(filter);
        }

        public async Task<List<Category>> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            return filter == null ?
                await _categoryDal.GetAll() :
                await _categoryDal.GetAll(filter);
        }

        public async Task Update(Category entity)
        {
            await _categoryDal.Update(entity);
        }
    }
}
