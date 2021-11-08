using AutoMapper;
using KTU_API.Autherization.Model;
using KTU_API.Data;
using KTU_API.Data.Dtos.Comments;
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
    [Route("api/paths/{pathId}/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRespository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IPathRepository _pathRepository;
        private readonly IAuthorizationService _authorizationService;

        public CommentController(ICommentRespository commentRepository, IMapper mapper, 
            IPathRepository pathRepository, IAuthorizationService authorizationService)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _pathRepository = pathRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<CommentDto>> GetAllAsync(int pathId)
        {
            var topics = await _commentRepository.GetAsync(pathId);
            return topics.Select(o => _mapper.Map<CommentDto>(o));
        }

        // /api/topics/1/posts/2
        [HttpGet("{commentId}")]
        public async Task<ActionResult<CommentDto>> GetAsync(int pathId, int commentId)
        {
            var comment = await _commentRepository.GetAsync(pathId, commentId);
            if (comment == null) return NotFound();

            return Ok(_mapper.Map<CommentDto>(comment));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<CommentDto>> PostAsync(int pathId, CreateCommentDto CommentDto)
        {
            var comment = await _pathRepository.Get(pathId);
            if (comment == null) return NotFound($"Couldn't find a topic with id of {pathId}");

            var pComment = _mapper.Map<Comment>(CommentDto);
            pComment.UserID = User.FindFirst(CustomClaims.UserID).Value;
            pComment.Path = await _pathRepository.Get(pathId);
            pComment.Likes = 0;
            pComment.Dislikes = 0;
            pComment.CreationDateUtc = DateTime.UtcNow;
            await _commentRepository.InsertAsync(pComment);

            return Created($"/api/paths/{pathId}/comments/{pComment.Id}", _mapper.Map<CommentDto>(pComment));
        }

        [HttpPut("{commentId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<CommentDto>> PostAsync(int pathId, int commentId, UpdateCommentDto CommentDto)
        {
            var comment = await _pathRepository.Get(pathId);
            if (comment == null) return NotFound($"Couldn't find a topic with id of {pathId}");

            var oldComment = await _commentRepository.GetAsync(pathId, commentId);
            if (oldComment == null)
                return NotFound();

            var result = await _authorizationService.AuthorizeAsync(User, comment, Constants.SameUsers);
            if (result.Succeeded)
            {
                _mapper.Map(CommentDto, oldComment);
                await _commentRepository.UpdateAsync(oldComment);
                return Ok(_mapper.Map<CommentDto>(oldComment));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{commentId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult> DeleteAsync(int pathId, int commentId)
        {
            var comment = await _commentRepository.GetAsync(pathId, commentId);
            if (comment == null)
                return NotFound();
            var result = await _authorizationService.AuthorizeAsync(User, comment, Constants.SameUsers);
            if (result.Succeeded)
            {
                await _commentRepository.DeleteAsync(comment);
                return NoContent();
            }
            else
                return BadRequest();
                // 204
            return NoContent();
        }
    }
}
