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

        public async Task<Customer> GetById(string id)
        {
            return await _customerDal.Get(x=>x.CustomerId==id);
        }

        public async Task<List<Customer>> GetAll()
        {
            return _customerDal.GetAll();
        }

        public async Task Update(Customer entity)
        {
            await _customerDal.Update(entity);
        }
    }
}
