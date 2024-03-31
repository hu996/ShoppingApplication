using Domain.Models;
using Infarastructure.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarastructure.Repositories
{
    public class OrderServices : IOrderRepository<Order>
    {
        private readonly ApplicationDbContext _context;

        public OrderServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                return true;
            }

            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Order> DisplayAllOrders()
        {
            return _context.Orders.AsNoTracking().ToList();
        }

        public Order GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Order order, Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
