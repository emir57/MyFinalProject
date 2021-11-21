using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public async Task Add(Customer entity)
        {
            await _customerDal.Add(entity);
        }

        public async Task Delete(Customer entity)
        {
            await _customerDal.Delete(entity);
        }

        public async Task<Customer> Get(Expression<Func<Customer, bool>> filter)
        {
            return await _customerDal.Get(filter);
        }

        public async Task<List<Customer>> GetAll(Expression<Func<Customer, bool>> filter = null)
        {
            return filter == null ?
                await _customerDal.GetAll() :
                await _customerDal.GetAll(filter);
        }

        public async Task Update(Customer entity)
        {
            await _customerDal.Update(entity);
        }
    }
}
