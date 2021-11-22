﻿using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ProductTest();
            //await CategoryTest();
        }

        private static async Task CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in await categoryManager.GetAll())
            {
                Console.WriteLine($"{category.CategoryName}");
            }
        }

        private static async Task ProductTest()
        {
            IProductService productManager = new ProductManager(new EfProductDal());
            foreach (var product in ((SuccessDataResult<List<ProductDetailDto>>)await productManager.GetProductDetails()).Data)
            {
                Console.WriteLine($"{product.ProductName} - {product.CategoryName}");
            }
        }
    }
}
