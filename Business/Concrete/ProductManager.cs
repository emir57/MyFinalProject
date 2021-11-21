using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task Add(Product entity)
        {
            await _productDal.Add(entity);
        }

        public async Task Delete(Product entity)
        {
            await _productDal.Delete(entity);
        }

        public async Task<Product> Get(Expression<Func<Product, bool>> filter)
        {
            return await _productDal.Get(filter);
        }

        public async Task<List<Product>> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            return filter == null ?
                await _productDal.GetAll() :
                await _productDal.GetAll(filter);
        }

        public async Task<List<Product>> GetAllByCategoryId(int categoryId)
        {
            return await _productDal.GetAll(a => a.CategoryId == categoryId);
        }

        public async Task<List<Product>> GetByUnitPrice(decimal minPrice, decimal maxPrice)
        {
            return await _productDal.GetAll(p => p.UnitPrice >= minPrice && p.UnitPrice <= maxPrice);
        }

        public async Task<List<ProductDetailDto>> GetProductDetails()
        {
            return await _productDal.GetProductDetails();
        }

        public async Task Update(Product entity)
        {
            await _productDal.Update(entity);
        }
    }
}
