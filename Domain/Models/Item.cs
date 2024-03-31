using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }

        public string Description { get; set; }

        public int QTY { get; set; }
        public decimal Price { get; set; }

        
        public ICollection<OrderDetails> OrderDetails { get; set; }

        public int UnitOfMeasureId { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
    }
}
