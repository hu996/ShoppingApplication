using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CurrencyExChangeRate
    {
        public string Currency { get; set; }
        public Decimal ExRate { get; set; }
    }
}
