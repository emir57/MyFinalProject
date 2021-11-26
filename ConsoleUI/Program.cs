using Business.Abstract;
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
            var result = await categoryManager.GetAll();
            foreach (var category in result.Data)
            {
                Console.WriteLine($"{category.CategoryName}");
            }
        }

        private static async Task ProductTest()
        {
            IProductService productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));
            var result = await productManager.GetProductDetails();
            if (result.Success)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine($"{product.ProductName} - {product.CategoryName}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
        }
    }
}
