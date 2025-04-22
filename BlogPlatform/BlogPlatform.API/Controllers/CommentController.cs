using BlogPlatform.Core.Dtos;
using BlogPlatform.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogPlatform.API.Controllers;

[Authorize]
[ApiController]
[Route("api/posts/{postId}/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly CommentRepository _commentRepo;

    public CommentsController(CommentRepository commentRepo)
    {
        _commentRepo = commentRepo;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetByPostId(int postId)
    {
        var comments = await _commentRepo.GetByPostIdAsync(postId);
        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> Add(int postId, [FromBody] CreateCommentDto commentDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var comment = await _commentRepo.AddAsync(commentDto, postId, userId);
        return CreatedAtAction(nameof(GetByPostId), new { postId }, comment);
    }
}