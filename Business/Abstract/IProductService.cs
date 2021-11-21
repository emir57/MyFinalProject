using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<List<Product>> GetAll(Expression<Func<Product, bool>> filter = null);

        Task<List<Product>> GetAllByCategoryId(int categoryId);
        Task<List<Product>> GetByUnitPrice(decimal minPrice, decimal maxPrice);

        Task<List<ProductDetailDto>> GetProductDetails();

        Task<Product> Get(Expression<Func<Product, bool>> filter);
        Task Add(Product entity);
        Task Update(Product entity);
        Task Delete(Product entity);
    }
}
