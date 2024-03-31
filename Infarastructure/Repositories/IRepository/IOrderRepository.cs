using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarastructure.Repositories.IRepository
{
    public interface IOrderRepository<T>where T : class
    {

        bool Add(T order);
        bool Delete(int Id);

        bool Update(T order ,Guid Id);
        List<T> DisplayAllOrders();

        T GetById(int Id);
    }
}
