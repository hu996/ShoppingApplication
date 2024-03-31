using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarastructure.Repositories.IRepository
{
    public interface GenralRepository<T> where T : class
    {
        T GetById(int Id);
        bool Add(T item);
        bool Delete(int ItemId);
        bool Update(T item);
        List<T> DisplayAllItems();
    }
}
