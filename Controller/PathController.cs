using AutoMapper;
using KTU_API.Autherization.Model;
using KTU_API.Data;
using KTU_API.Data.Dtos.Paths;
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
    [Route("api/paths")]
    public class PathController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPathRepository _pathRepository;
        private readonly IAuthorizationService _authorizationService;

        public PathController(IPathRepository pathRepository, IMapper mapper, IAuthorizationService authorizationService)
        {
            _pathRepository = pathRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<PathDto>> GetAll()
        {
            return (await _pathRepository.GetAll()).Select(o => _mapper.Map<PathDto>(o));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PathDto>> Get(int id)
        {
            var topic = await _pathRepository.Get(id);
            if (topic == null) return NotFound($"Path with id '{id}' not found.");

            //return _mapper.Map<TopicDto>(topic);
            return Ok(_mapper.Map<PathDto>(topic));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<PathDto>> Post(CreatePathDto pathDto)
        {
            var path = _mapper.Map<Path>(pathDto);
            path.UserID = User.FindFirst(CustomClaims.UserID).Value;
            await _pathRepository.Create(path);

            // 201
            // Created topic
            return Created($"/api/topics/{path.Id}", _mapper.Map<PathDto>(path));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<PathDto>> Put(int id, UpdatePathDto pathDto)
        {
            var path = await _pathRepository.Get(id);
            if (path == null) return NotFound($"Path with id '{id}' not found.");

            var result = await _authorizationService.AuthorizeAsync(User, path, Constants.SameUsers);

            if (result.Succeeded)
            {
                _mapper.Map(pathDto, path);

                await _pathRepository.Put(path);

                return Ok(_mapper.Map<PathDto>(path));
            }
            else
                return Forbid("You don't have access to this object");
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<PathDto>> Delete(int id)
        {
            var path = await _pathRepository.Get(id);

            if (path == null) return NotFound($"Path with id '{id}' not found.");

            var result = await _authorizationService.AuthorizeAsync(User, path, Constants.SameUsers);

            if (result.Succeeded)
            {
                await _pathRepository.Delete(path);
                return NoContent();
            }
            else
            {
                return Forbid();
            }
            // 204
            
        }
    }
}
