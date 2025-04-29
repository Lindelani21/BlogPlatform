using BlogPlatform.Core.Dtos;
using BlogPlatform.Core.Models;
using BlogPlatform.Infrastructure.Data;
using BlogPlatform.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;


namespace BlogPlatform.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly BlogContext _context;
    private readonly PostRepository _postRepo;
    private readonly ILogger<PostsController> _logger;

    public PostsController(PostRepository postRepo, ILogger<PostsController> logger)
    {
        _postRepo = postRepo;
        _logger = logger;
        
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var posts = await _postRepo.GetAllAsync();
            return Ok(posts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all posts");
            return StatusCode(500, "Internal server error");
        }
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var post = await _postRepo.GetByIdAsync(id);
            return post == null ? NotFound() : Ok(post);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting post {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePostDto postDto)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var post = await _postRepo.CreateAsync(postDto, userId);
            return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating post");
            return BadRequest("Error creating post");
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetPaginated(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        var (posts, totalCount) = await _postRepo.GetPaginatedAsync(page, pageSize);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(new
        {
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        }));

        return Ok(posts);
    }

}



