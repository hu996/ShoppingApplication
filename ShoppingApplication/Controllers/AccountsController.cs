using Infarastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.Dtos.AccountsDtos;
using System;
using System.Threading.Tasks;

namespace ShoppingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController(RoleManager<IdentityRole>rolemanager, UserManager<ApplicationUser> usermanager)
        {
            _roleManager = rolemanager;
            _userManager = usermanager;
        }


        [HttpPost]
        [Route("CreateRole")]
        [Authorize("Admin")]
        public async Task<IActionResult> CreateRole(RoleDto role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Role");
            }


            var NewRole = new IdentityRole
            {
                Id=Guid.NewGuid().ToString(),
                Name = role.RoleName,
            };

            var SaveRole =await _roleManager.CreateAsync(NewRole);
            return Ok(new {status=200,Message= "Role Created Successfully",Role= NewRole.Name });
        }

        


         
    }
}
