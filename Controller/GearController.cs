using AutoMapper;
using KTU_API.Autherization.Model;
using KTU_API.Data;
using KTU_API.Data.Dtos.Needed_Gears;
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
    [Route("api/paths/{pathId}/gear")]
    public class GearController : ControllerBase
    {
        private readonly IGearRepository _gearRepository;
        private readonly IMapper _mapper;
        private readonly IPathRepository _pathRepository;
        private readonly IAuthorizationService _authorizationService;

        public GearController(IGearRepository gearRepository, IMapper mapper, IPathRepository pathRepository, IAuthorizationService authorizationService)
        {
            _gearRepository = gearRepository;
            _mapper = mapper;
            _pathRepository = pathRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IEnumerable<NeededGearDto>> GetAllAsync(int pathId)
        {
            var topics = await _gearRepository.GetAsync(pathId);
            return topics.Select(o => _mapper.Map<NeededGearDto>(o));
        }

        // /api/topics/1/posts/2
        [HttpGet("{gearId}")]
        public async Task<ActionResult<NeededGearDto>> GetAsync(int pathId, int gearId)
        {
            var gear = await _gearRepository.GetAsync(pathId, gearId);
            if (gear == null) return NotFound();

            return Ok(_mapper.Map<NeededGearDto>(gear));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<NeededGearDto>> PostAsync(int pathId, CreateNeededGearDto NeededGearDto)
        {
            var gear = await _pathRepository.Get(pathId);
            if (gear == null) return NotFound($"Couldn't find a topic with id of {pathId}");


            var nGear = _mapper.Map<Needed_Gear>(NeededGearDto);
            nGear.Path = await _pathRepository.Get(pathId);
            nGear.UserID = User.FindFirst(CustomClaims.UserID).Value;
            await _gearRepository.InsertAsync(nGear);

            return Created($"/api/paths/{pathId}/gear/{nGear.Id}", _mapper.Map<NeededGearDto>(nGear));
        }

        [HttpPut("{gearId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<NeededGearDto>> PostAsync(int pathId, int gearId, UpdateNeededGearDto NeededGearDto)
        {
            var gear = await _pathRepository.Get(pathId);
            if (gear == null) return NotFound($"Couldn't find a topic with id of {pathId}");

            var oldGear = await _gearRepository.GetAsync(pathId, gearId);
            if (oldGear == null)
                return NotFound();

            var result = await _authorizationService.AuthorizeAsync(User, gear, Constants.SameUsers);

            if (result.Succeeded)
            {
                _mapper.Map(NeededGearDto, oldGear);

                await _gearRepository.UpdateAsync(oldGear);

                return Ok(_mapper.Map<NeededGearDto>(oldGear));
            }
            else
                return BadRequest();
        }

        [HttpDelete("{gearId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult> DeleteAsync(int pathId, int gearId)
        {
            var gear = await _gearRepository.GetAsync(pathId, gearId);
            if (gear == null)
                return NotFound();

            var result = await _authorizationService.AuthorizeAsync(User, gear, Constants.SameUsers);

            if (result.Succeeded)
            {
                await _gearRepository.DeleteAsync(gear);
                return NoContent();
            }
            else
                return BadRequest();
                // 204
        }
    }
}
