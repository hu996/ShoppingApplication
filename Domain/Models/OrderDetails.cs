using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public decimal ItemPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public int orderId { get; set; }

        [ForeignKey("orderId")]
        public Order order { get; set; }

        public int ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
    }
}
