using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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
        [ValidationAspect(typeof(ProductValidator))]
        public async Task<IResult> Add(Product entity)
        {

            await _productDal.Add(entity);
            return new SuccessResult(Messages.ProductAdded);
        }

        public async Task Delete(Product entity)
        {
            await _productDal.Delete(entity);
        }
        public async Task<IDataResult<List<Product>>> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return filter == null ?
                new SuccessDataResult<List<Product>>(await _productDal.GetAll(),Messages.ProductsListed) :
                new SuccessDataResult<List<Product>>(await _productDal.GetAll(filter), Messages.ProductsListed);
        }

        public async Task<IDataResult<List<Product>>> GetAllByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(await _productDal.GetAll(a => a.CategoryId == categoryId));
        }

        public async Task<IDataResult<Product>> GetById(int productId)
        {
            return new SuccessDataResult<Product>(await _productDal.Get(p => p.ProductId == productId));
        }

        public async Task<IDataResult<List<Product>>> GetByUnitPrice(decimal minPrice, decimal maxPrice)
        {
            return new SuccessDataResult<List<Product>>(await _productDal.GetAll(p => p.UnitPrice >= minPrice && p.UnitPrice <= maxPrice));
        }

        public async Task<IDataResult<List<ProductDetailDto>>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(await _productDal.GetProductDetails());
        }

        public async Task Update(Product entity)
        {
            await _productDal.Update(entity);
        }
    }
}
