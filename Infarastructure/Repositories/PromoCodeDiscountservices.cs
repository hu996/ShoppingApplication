using Domain.Models;
using Infarastructure.Repositories.IRepository;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infarastructure.Repositories
{
    public class PromoCodeDiscountservices : IPromoCodeDiscountRepo
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IConfiguration _config;
        private readonly IDatabase _db;
        private int Flag = 0;


        public PromoCodeDiscountservices(IConfiguration config)
        {
            _config = config;
            _conn = ConnectionMultiplexer.Connect(_config["RedisConfig:Host"]);
            _db = _conn.GetDatabase();

        }

        public decimal CheckPromoCode(string promoCode)
        {

            var AllPromoCode = _db.StringGet("Currency");
            var DesPromoCode = JsonSerializer.Deserialize<List<PromoCodeDiscount>>(AllPromoCode);

            const decimal ExRateConstantValue = 1;


            foreach (var Code in DesPromoCode)
            {
                if (Code.PromoCode == promoCode)
                {
                    Flag = 1;
                    return Code.PromoValue;
                }



            }

            return ExRateConstantValue;
        }

        public bool SavePromoCode(PromoCodeDiscount svcode)
        {
            try
            {
                CheckPromoCode(svcode.PromoCode);
                if (Flag == 1)
                {
                    return false;
                }

                var AllCodes = _db.StringGet("PromoCodes");
                var DesCodes = JsonSerializer.Deserialize<List<PromoCodeDiscount>>(AllCodes);
                DesCodes.Add(new PromoCodeDiscount { PromoCode = svcode.PromoCode, PromoValue = svcode.PromoValue });

                var SerData = JsonSerializer.Serialize(DesCodes);
                _db.StringSet("PromoCodes", SerData);

                //_db.KeyExpire("PromoCodes", DateTime.Parse(_config["RedisConfig:ExpiryDate"]));

                return true;

            }
            catch (Exception Ex)
            {
                return false;
            }

        }




        public List<PromoCodeDiscount> GetAll()
        {
            try
            {
                var AllCodes = _db.StringGet("Currency");
                var DesCodes = JsonSerializer.Deserialize<List<PromoCodeDiscount>>(AllCodes);

                return DesCodes;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
    }
}

   

