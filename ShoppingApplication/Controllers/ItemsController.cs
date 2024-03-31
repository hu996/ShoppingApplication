using Domain.Models;
using Infarastructure.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Dtos.ShoppingDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private readonly GenralRepository<Item> _Item;

        public ItemsController(GenralRepository<Item> item)
        {
            _Item = item;
        }


        [HttpGet]
        [Route("Displayitems")]

        public IActionResult DisplayAllItems()
        {
            //List<Item> items = _Item.DisplayAllItems();
            return Ok(_Item.DisplayAllItems());
        }

        [HttpPost]
        [Route("CreateItem")]
        public async Task<IActionResult> CreateItem(ItemDto itemdto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var NewItem = new Item
            {
                Description = itemdto.Description,
                ItemName = itemdto.ItemName,
                Price = itemdto.Price,
                QTY = itemdto.QTY,
                UnitOfMeasureId=itemdto.UnitOfMeasureId,
                
                

            };
            var saveItem = _Item.Add(NewItem);
            if (saveItem == true)
            {
                return Ok(NewItem);
            }
            return BadRequest("This Item Already Exist");
        }


        [HttpDelete]
        [Route("DelteItem")]
        public IActionResult DeleteItem(int Id)
        {
          var item=  _Item.Delete(Id);
            if (item == true)
            {
                return Ok("Item Deleted Successfully");
            }

            return NotFound("Item Not Found");
        }




        [HttpPut]
        [Route("UpdateItem")]

        public IActionResult UpdateItem(ItemDto item)
        {
            var i = _Item.GetById(item.Id);
            if(i != null)
            {
                i.ItemName = item.ItemName;
                i.Price = item.Price;
                i.QTY = item.QTY;
                i.Price = item.Price;
                i.Description = item.Description;

                var UpdateItem = _Item.Update(i);
                if (UpdateItem == true)
                {
                    return Ok(_Item.GetById(item.Id));
                }
            }
           

            return NotFound("Item Not found To Delete");
        }
    }
}
