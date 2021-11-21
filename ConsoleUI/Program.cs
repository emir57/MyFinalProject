using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IProductService productManager = new ProductManager(new EfProductDal());
            foreach (var product in await productManager.GetByUnitPrice(30,50))
            {
                Console.WriteLine($"{product.ProductName} {product.UnitPrice}$");
            }
        }
    }
}
