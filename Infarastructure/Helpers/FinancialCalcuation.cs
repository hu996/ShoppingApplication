using Domain.Models;
using Infarastructure.Repositories.IRepository;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infarastructure.Helpers
{

    
    public static class FinancialCalcuation
    {
        


        public static Decimal CalculateTotalPrice(int itemQnty,decimal Price,string PromCode )
        {
            
            if(PromCode is not null)
            {
                var PromoCodeValue = GetPromoCodeValue(PromCode);
                if(PromoCodeValue != null && PromoCodeValue > 0)
                {
                    return itemQnty * Price - PromoCodeValue ;
                }
                return itemQnty * Price;
                
            }
            return itemQnty * Price;
        }

        public static Decimal GetPromoCodeValue(string PromoCode)
        {
            ConnectionMultiplexer conn = ConnectionMultiplexer.Connect("154.41.209.74:6379");
            IDatabase _db = conn.GetDatabase();

            if(PromoCode is not null)
            {
                var promocodes = _db.StringGet("PromoCode");

                var desCodes=JsonSerializer.Deserialize<List<PromoCodeDiscount>>(promocodes);

                foreach(var discount in desCodes )
                {
                    if (string.Equals(discount.PromoCode, PromoCode, StringComparison.OrdinalIgnoreCase))
                    {
                        return discount.PromoValue;
                    }
                }
            }

            return 0;
        }


        public static decimal CalculateForignPrice(decimal ExRate)
        {
            return 0;
        }
    }

}
