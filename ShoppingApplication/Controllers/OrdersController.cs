using Domain.Models;
using Infarastructure;
using Infarastructure.Helpers;
using Infarastructure.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Dtos.ShoppingDto;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShoppingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    
    public class OrdersController : ControllerBase
    {

        private readonly IOrderRepository<Order> _orders;
        private readonly IOrderDetails _OD;
        private readonly IOrderRepository<OrderDetails> _ordersDetails;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(IOrderRepository<Order> order,UserManager<ApplicationUser> usermanager,
            IOrderRepository<OrderDetails> orderDetails,IOrderDetails OD)
        {
            _orders = order;
            _ordersDetails = orderDetails;
            _userManager = usermanager;
            _OD = OD;
        }

        [HttpPost]
        [Route("CReateOrder")]
        public async Task<IActionResult> CreateOrder(OrderDto order)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var OrderId = _OD.GetLAstOrderId();
            var RequestDate = DateTime.Now;
            var NewOrder = new Order
            {
                CurrencyCode = order.CurrencyCode,
                RequestDate = RequestDate,
                CloseDate=DateTime.Now,
                DiscountPromoCode=order.DiscountPromoCode,
                Status=RequestDate < order.CloseDate?"open":"close",
                DiscountValue=FinancialCalcuation.GetPromoCodeValue(order.DiscountPromoCode),
                TotalPrice=FinancialCalcuation.CalculateTotalPrice(order.Quantity,order.ItemPrice,order.DiscountPromoCode),
               

            };
          var saveorder=  _orders.Add(NewOrder);
           
            var SaveOrderDetails = _ordersDetails.Add(new OrderDetails { ItemId=order.itemId,TotalPrice = NewOrder.TotalPrice,orderId= OrderId ,Quantity=order.Quantity ,ItemPrice=order.ItemPrice}) ;
            if (saveorder == true && SaveOrderDetails==true)
            {
                return Ok(order);
            }
            return BadRequest(ModelState);
        }


        [HttpGet]
        [Route("DisplayOrders")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DisplayAllOrders()
        {
            
            return Ok(_orders.DisplayAllOrders());
        }




    }
}
