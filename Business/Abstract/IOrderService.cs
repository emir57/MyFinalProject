using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        Task<List<Order>> GetAll(Expression<Func<Order, bool>> filter = null);
        Task<Order> Get(Expression<Func<Order, bool>> filter);
        Task Add(Order entity);
        Task Update(Order entity);
        Task Delete(Order entity);
    }
}
