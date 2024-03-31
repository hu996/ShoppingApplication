using Domain.Models;
using Infarastructure.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodeController : ControllerBase
    {
        private readonly IPromoCodeDiscountRepo _code ;

        public PromoCodeController(IPromoCodeDiscountRepo code)
        {
            _code = code;
        }


        [HttpGet]
        [Route("DisplayCodes")]

        public IActionResult DisplayAllCodes()
        {
            return Ok(_code.GetAll());
        }


        [HttpPost]

        [Route("CreateCode")]

        public IActionResult CreateCode(PromoCodeDiscount promoCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            if (_code.SavePromoCode(promoCode))
            {
                return Ok(new {Message="Success" , promoCode});
            }
            return BadRequest(ModelState);
        }

    }
}
