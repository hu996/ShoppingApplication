using Infarastructure;
using Infarastructure.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoppingApplication.Dtos.AccountsDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IConfiguration config, UserManager<ApplicationUser> usermanager)
        {
            _config = config;
            _userManager = usermanager;
        }

        [HttpPost("Register")]
        //[Authorize("Admin")]
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Newuser = new ApplicationUser
            {
                UserName = user.UserName,
                Name = user.Name,
                

            };
            var SaveUser = await _userManager.CreateAsync(Newuser,user.Password);
            //UserRole userRole = UserRole.Admin;
            if (SaveUser.Succeeded)
            {
                
                //var AddUserRole = await _userManager.AddToRoleAsync(Newuser,userRole.ToString());
                var AddUserRole = await _userManager.AddToRoleAsync(Newuser,user.Role);
                return Ok(new { Status = 200, Name = Newuser.Name, UserName = Newuser.UserName, Role = user.Role, Message = "User Created Successfully" });
            }
            return BadRequest(SaveUser.Errors.FirstOrDefault());
        }



        [HttpPost("Login")]

        public async Task<IActionResult> Login(LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            ApplicationUser User = await _userManager.FindByNameAsync(login.UserName);
            if(User != null)
            {
                var checkPassowrd = await _userManager.CheckPasswordAsync(User, login.Password);
                if (checkPassowrd)
                {
                    var Claims = new List<Claim>();
                    Claims.Add(new Claim(ClaimTypes.Name, User.UserName));
                    Claims.Add(new Claim(ClaimTypes.NameIdentifier, User.Id));
                    Claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));

                    var roles = await _userManager.GetRolesAsync(User);

                    foreach(var role in roles)
                    {
                        Claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                    SigningCredentials SigningCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);


                    JwtSecurityToken MyToken = new JwtSecurityToken(
                         issuer: _config["JWT:ValidIssuar"],
                        audience: _config["JWT:ValidAudience"],
                        claims: Claims,
                        expires:DateTime.Now.AddHours(1),
                        signingCredentials: SigningCredentials 


                        );
                    return Ok(new
                    {
                        token=new JwtSecurityTokenHandler().WriteToken(MyToken),
                        Expiration=MyToken.ValidTo,
                        UserName=login.UserName,
                        Role = roles[0]
                        
                    });
                }
                return Unauthorized();
            }
            return Ok();
        }
    }
}
