using Domain.Models;
using Infarastructure.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarastructure.Repositories
{
    public class OrderDetailServices : IOrderRepository<OrderDetails>,IOrderDetails
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailServices(ApplicationDbContext context)
        {
            _context=context;
        }
        public bool Add(OrderDetails orderDetails)
        {
            try
            {
                _context.OrdersDetails.Add(orderDetails);
                _context.SaveChanges();
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetails> DisplayAllOrders()
        {
            throw new NotImplementedException();
        }

        public OrderDetails GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public int GetLAstOrderId()
        {
            var result=_context.Orders.OrderByDescending(a => a.Id).FirstOrDefault();
            return result.Id;
        }

        public bool Update(OrderDetails order, Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
