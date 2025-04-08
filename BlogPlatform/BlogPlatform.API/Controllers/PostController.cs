using BlogPlatform.Core.Models;
using BlogPlatform.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly BlogContext _context;

    public PostsController(BlogContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<BlogPost>>> GetPosts()
    {
        return await _context.Posts
            .Include(p => p.Author)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<BlogPost>> CreatePost(BlogPost post)
    {
        
        post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPosts), post);
    }


}
