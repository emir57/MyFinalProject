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
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public async Task Add(Order entity)
        {
            await _orderDal.Add(entity);
        }

        public async Task Delete(Order entity)
        {
            await _orderDal.Delete(entity);
        }

        public async Task<Order> GetById(string id)
        {
            return await _orderDal.Get(x=>x.CustomerId == id);
        }

        public async Task<List<Order>> GetAll()
        {
            return await _orderDal.GetAll();
        }

        public async Task Update(Order entity)
        {
            await _orderDal.Update(entity);
        }
    }
}
