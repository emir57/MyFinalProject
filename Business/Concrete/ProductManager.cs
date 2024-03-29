﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
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
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Aspects.Autofac.Exception;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }


        [CacheRemoveAspect("IProductService.Get")]
        [SecuredOperation("Admin,Editor")]
        [ValidationAspect(typeof(ProductValidator))]
        public async Task<IResult> Add(Product entity)
        {
            var result = BusinessRules.Run(
                await CheckIfProductCountOfCategoryCorrect(entity.CategoryId),
                await CheckProductNameAsync(entity.ProductName),
                await CheckIfCategoryLimitExceed());
            if (result!=null)
            {
                return result;
            }
            await _productDal.Add(entity);
            return new SuccessResult(Messages.ProductAdded);
            
        }
        [CacheRemoveAspect("IProductService.Get")]
        [ValidationAspect(typeof(ProductValidator))]
        public async Task Update(Product entity)
        {
            await _productDal.Update(entity);
        }
        [CacheRemoveAspect("IProductService.Get")]
        public async Task Delete(Product entity)
        {
            await _productDal.Delete(entity);
        }
        [CacheAspect(60)]
        [PerformanceAspect(1)]
        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<List<Product>>> GetAll()
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(await _productDal.GetAll(), Messages.ProductsListed);
                
        }

        public async Task<IDataResult<List<Product>>> GetAllByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(await _productDal.GetAll(a => a.CategoryId == categoryId));
        }
        [CacheAspect]
        //[PerformanceAspect(2)]
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
    
    
        private async Task<IResult> CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var products = await _productDal.GetAll(p => p.CategoryId == categoryId);
            if (products.Count >= 10)
            {
                throw new Exception(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private async Task<IResult> CheckProductNameAsync(string productName)
        {
            var product = await _productDal.GetAll(p => p.ProductName == productName);
            if (product.Any())
            {
                throw new Exception(Messages.ProductCheckForName);
            }
            return new SuccessResult();
        }
        private async Task<IResult> CheckIfCategoryLimitExceed()
        {
            var categories = await _categoryService.GetAll();
            if (categories.Data.Count>15)
            {
                throw new Exception(Messages.CategoryLimitExceed);
            }
            return new SuccessResult();
            
        }

        [TransactionalScopeAspect]
        public async Task<IResult> AddTransactionalTest(Product entity)
        {
            
            await Add(entity);
            if (entity.UnitPrice < 10)
            {
                throw new Exception();
            }
            await Add(entity);
            return null;
        }
    }
}
