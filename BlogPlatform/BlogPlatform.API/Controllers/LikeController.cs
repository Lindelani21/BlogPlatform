using BlogPlatform.Core.Models;
using BlogPlatform.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlogPlatform.Core.Dtos;

namespace BlogPlatform.API.Controllers;

[Authorize]
[ApiController]
[Route("api/posts/{postId}/[controller]")]
public class LikesController : ControllerBase
{
    private readonly LikeRepository _likeRepo;

    public LikesController(LikeRepository likeRepo)
    {
        _likeRepo = likeRepo;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetStatus(int postId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var status = await _likeRepo.GetLikeStatus(postId, userId);
        return Ok(status);
    }

    [HttpPost]
    public async Task<IActionResult> ToggleLike(int postId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _likeRepo.ToggleLike(postId, userId);
        return NoContent();
    }
}