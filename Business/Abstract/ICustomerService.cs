using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAll(Expression<Func<Customer, bool>> filter = null);
        Task<Customer> Get(Expression<Func<Customer, bool>> filter);
        Task Add(Customer entity);
        Task Update(Customer entity);
        Task Delete(Customer entity);
    }
}
