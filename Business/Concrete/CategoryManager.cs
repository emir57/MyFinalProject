using Business.Abstract;
using Core.Utilities.Results;
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

        public async Task<IDataResult<Category>> Get(Expression<Func<Category, bool>> filter)
        {
            var category = await _categoryDal.Get(filter);
            return new SuccessDataResult<Category>(category);
        }

        public async Task<IDataResult<List<Category>>> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            var categories = filter == null ?
                await _categoryDal.GetAll() :
                await _categoryDal.GetAll(filter);
            return new SuccessDataResult<List<Category>>(categories);
        }

        public async Task<IDataResult<Category>> GetById(int categoryId)
        {
            var category = await _categoryDal.Get(c => c.CategoryId == categoryId);
            return new SuccessDataResult<Category>(category);
        }

        public async Task Update(Category entity)
        {
            await _categoryDal.Update(entity);
        }
    }
}
