using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarastructure.Repositories.IRepository
{
    public  interface ICachCurrencyRepo
    {

        public decimal CheckCurrencyExist(string Currency);
        public bool SaveCurrency(CurrencyExChangeRate svcurr);

        public List<CurrencyExChangeRate> GetAll();
    }
}
