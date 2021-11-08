using AutoMapper;
using KTU_API.Autherization;
using KTU_API.Autherization.Model;
using KTU_API.Data.Dtos.Users;
using KTU_API.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KTU_API.Data.Entities.User;

namespace KTU_API.Controller
{
    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public LoginController(UserManager<User> userManager, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(CreateUsersDto createUserDto)
        {
            var user = await _userManager.FindByNameAsync(createUserDto.name);
            if (user != null)
                return BadRequest("User exsists");

            var newUser = new User
            {
                UserName = createUserDto.name
            };

            var createdUser = await _userManager.CreateAsync(newUser, createUserDto.password);

            if (!createdUser.Succeeded)
                return BadRequest("Something happened during creation, please check back later");

            await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return CreatedAtAction(nameof(Register), _mapper.Map<UserDto>(newUser));
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
                return BadRequest("User name or password is invalid");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return BadRequest("User name or password is invalid");

            var acessToken =  await _tokenService.CreateUserToken(user);

            return Ok(new SLoginResponse(acessToken));
        }
    }
}
