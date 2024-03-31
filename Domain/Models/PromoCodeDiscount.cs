using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PromoCodeDiscount
    {
        public string PromoCode { get; set; }

        private decimal _PromoValue { get;set; }

        public decimal PromoValue
        {
            set
            {
                if (value <= 0)
                {
                    _PromoValue = 1;
                }
            }
            get
            {
                return _PromoValue;
            }
        } 
    }
}
