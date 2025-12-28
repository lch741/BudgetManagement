using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController(ITokenService tokenService,UserManager<AppUser> UserManager,SignInManager<AppUser> SignInManager) : ControllerBase
    {
        private readonly UserManager<AppUser> _UserManager = UserManager;
        private readonly SignInManager<AppUser> _SignInManager = SignInManager;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto RegisterDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                AppUser user = new AppUser
                {
                    UserName = RegisterDto.Username,
                    Email = RegisterDto.Email,
                };

                var createResult = await _UserManager.CreateAsync(user,RegisterDto.Password);

                if (createResult.Succeeded)
                {
                    var roleResult = await _UserManager.AddToRoleAsync(user,"User");
                    if(roleResult.Succeeded){
                        return Ok(
                            new NewUserDto
                            {
                                Username = user.UserName!,
                                Email = user.Email!,
                                Token = _tokenService.CreateToken(user)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500,roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500,createResult.Errors);
                }
            }catch(Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto LoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var User = await _UserManager.Users.FirstOrDefaultAsync(x=>x.UserName==LoginDto.Username);

            if (User == null)
            {
                return Unauthorized("Invalid Username!");
            }

            var result = await _SignInManager.CheckPasswordSignInAsync(User,LoginDto.Password,false);

            if(!result.Succeeded)
            {
                return Unauthorized("Invalid Password!");
            }

            return Ok(new NewUserDto
            {
                Username = User.UserName!,
                Email = User.Email!,
                Token = _tokenService.CreateToken(User)
            });
        }

    }
}