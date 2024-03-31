using Domain.Models;
using Infarastructure.Helpers;
using Infarastructure.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace ShoppingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {

        private readonly ICachCurrencyRepo _ExRatecach;

        public CurrencyController(ICachCurrencyRepo ExRatecach)
        {
            _ExRatecach = ExRatecach;
        }

        [HttpPost]
        [Route("AddCurrency")]

        public IActionResult CreateCurrncy(CurrencyExChangeRate ExRate )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var NewCurr= _ExRatecach.SaveCurrency(ExRate);

            if (NewCurr == true)
            {
                return Ok(new { Message = "Success", Name = ExRate.Currency, Value = ExRate.ExRate });
            }

            return BadRequest();
        }


        [HttpGet]
        [Route("DisplayCurrency")]


        public IActionResult DisplayCurrncies()
        {
            return Ok(_ExRatecach.GetAll());
        }
    }
}
