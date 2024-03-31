using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarastructure.Repositories.IRepository
{
    public interface IPromoCodeDiscountRepo
    {
        public decimal CheckPromoCode(string promoCode);

        public bool SavePromoCode(PromoCodeDiscount svcode);

        public List<PromoCodeDiscount> GetAll();


    }
}
