using Core.Utilities.Results;
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
        Task Update(Product entity);
        Task Delete(Product entity);

        Task<IDataResult<List<ProductDetailDto>>> GetProductDetails();
        Task<IDataResult<List<Product>>> GetByUnitPrice(decimal minPrice, decimal maxPrice);
        Task<IDataResult<List<Product>>> GetAllByCategoryId(int categoryId);
        Task<IDataResult<List<Product>>> GetAll(Expression<Func<Product, bool>> filter = null);
        Task<IResult> Add(Product entity);
        Task<IDataResult<Product>> GetById(int productId);
    }
}
