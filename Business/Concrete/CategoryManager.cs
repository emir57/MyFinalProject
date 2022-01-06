using Business.Abstract;
using Core.Aspects.Autofac.Caching;
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
        [CacheRemoveAspect("get")]
        public async Task Add(Category entity)
        {
            await _categoryDal.Add(entity);
        }
        [CacheRemoveAspect("get")]
        public async Task Delete(Category entity)
        {
            await _categoryDal.Delete(entity);
        }
        [CacheAspect]
        public async Task<IDataResult<List<Category>>> GetAll()
        {
            var categories = await _categoryDal.GetAll();

            return new SuccessDataResult<List<Category>>(categories);
        }
        [CacheAspect]
        public async Task<IDataResult<Category>> GetById(int categoryId)
        {
            var category = await _categoryDal.Get(c => c.CategoryId == categoryId);
            return new SuccessDataResult<Category>(category);
        }
        [CacheRemoveAspect("get")]
        public async Task Update(Category entity)
        {
            await _categoryDal.Update(entity);
        }
    }
}
