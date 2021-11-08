using AutoMapper;
using KTU_API.Autherization.Model;
using KTU_API.Data.Dtos.Paths;
using KTU_API.Data.Dtos.Users;
using KTU_API.Data.Entities;
using KTU_API.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTU_API.Controller
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationService _authorizationService;

        public UserController(IUserRepository userRepository, IMapper mapper, IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return (await _userRepository.GetAll()).Select(o => _mapper.Map<UserDto>(o));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return NotFound($"Topic with id '{id}' not found.");

            //return _mapper.Map<TopicDto>(topic);
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<UserDto>> Post(CreateUsersDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _userRepository.Create(user);

            // 201
            // Created topic
            return Created($"/api/topics/{user.Id}", _mapper.Map<UserDto>(user));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<UserDto>> Put(int id, UpdateUserDto userDto)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return NotFound($"Topic with id '{id}' not found.");

            if (user.Id == User.FindFirst(CustomClaims.UserID).Value ||
                User.IsInRole(UserRoles.Admin))
            {
                _mapper.Map(userDto, user);

                await _userRepository.Put(user);

                return Ok(_mapper.Map<UserDto>(user));
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<UserDto>> Delete(int id)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return NotFound($"Topic with id '{id}' not found.");

            if (user.Id == User.FindFirst(CustomClaims.UserID).Value ||
            User.IsInRole(UserRoles.Admin))
            {
                await _userRepository.Delete(user);
            }
            // 204
            return NoContent();
        }
    }
}
