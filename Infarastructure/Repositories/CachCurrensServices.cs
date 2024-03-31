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
    public class CachCurrensServices:ICachCurrencyRepo
    {

        private readonly ConnectionMultiplexer _conn;
        private readonly IConfiguration _config;
        private readonly IDatabase _db;

        public CachCurrensServices(IConfiguration config)
        {
            _config = config;
            _conn = ConnectionMultiplexer.Connect(_config["RedisConfig:Host"]);
            _db = _conn.GetDatabase();

        }


        public int Flag = 0;
        public decimal CheckCurrencyExist(string Currency)
        {
            var AllCurrency = _db.StringGet("Currency");
            var DesCurrency = JsonSerializer.Deserialize<List<CurrencyExChangeRate>>(AllCurrency);

            const decimal ExRateConstantValue = 1;


            foreach (var currency in DesCurrency)
            {
                if (currency.Currency == Currency)
                {
                    Flag = 1;
                    return currency.ExRate;
                }



            }

            return ExRateConstantValue;
        }




        public bool SaveCurrency(CurrencyExChangeRate svcurr)
        {
            try
            {
                CheckCurrencyExist(svcurr.Currency);
                if (Flag == 1)
                {
                    return false;
                }

                var AllCurrency = _db.StringGet("Currency");
                var DesCurrency = JsonSerializer.Deserialize<List<CurrencyExChangeRate>>(AllCurrency);
                DesCurrency.Add(new CurrencyExChangeRate { Currency = svcurr.Currency, ExRate = svcurr.ExRate });

                var SerData = JsonSerializer.Serialize(DesCurrency);
                _db.StringSet("Currency", SerData);
                _db.KeyExpire("Currency", DateTime.Parse(_config["RedisConfig:ExpiryDate"]));
                
                return true;

            }
            catch (Exception Ex)
            {
                return false;
            }



        }

        public List<CurrencyExChangeRate> GetAll()
        {
            try
            {
                var AllCurrency = _db.StringGet("Currency");
                var DesCurrency = JsonSerializer.Deserialize<List<CurrencyExChangeRate>>(AllCurrency);
                return DesCurrency;
            }

            catch(Exception ex)
            {
                return null;
            }
        }
    }
}

