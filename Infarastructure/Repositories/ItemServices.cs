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
    public class ItemServices : GenralRepository<Item>
    {
        private readonly ApplicationDbContext _context;
        public ItemServices(ApplicationDbContext context)
        {
            _context = context;   
        }
        public bool Add(Item item)
        {
            var CreateItem = _context.Items.Add(item);
           _context.SaveChanges();
           
            return true;
        }

        public bool Delete(int ItemId)
        {
            var ItemToDelete = _context.Items.Find(ItemId);
            if(ItemToDelete != null)
            {
               _context.Items.Remove(ItemToDelete);
                _context.SaveChanges();
                return true;

            }

            throw new InvalidOperationException("Not Found Item To Delete");
        }

        public List<Item> DisplayAllItems()
        {
            return _context.Items.AsNoTracking().ToList();
        }

        public bool Update(Item item)
        {
            var ItemToUpdate = _context.Items.Find(item.Id);
            if(ItemToUpdate != null)
            {
                _context.Entry(ItemToUpdate).CurrentValues.SetValues(item);
                _context.SaveChanges();
                return true;
            }

            throw new InvalidOperationException("Not Found Item To Update");
        }


        public Item GetById(int Id)
        {
            return _context.Items.FirstOrDefault(item => item.Id == Id);
        }
    }
}
