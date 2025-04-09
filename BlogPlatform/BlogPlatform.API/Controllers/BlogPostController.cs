using BlogPlatform.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogPlatform.API.Controllers
{
    //Controllers/BlogPostsController.cs
    //[ApiController]
    //[Route("api/[controller]")]
    //[Authorize]
    //public class BlogPostsController : ControllerBase
    //{
    //    private readonly IBlogPostRepository _blogPostRepository;
    //    private readonly IUserRepository _userRepository;
    //    private readonly INotificationService _notificationService;

    //    public BlogPostsController(
    //        IBlogPostRepository blogPostRepository,
    //        IUserRepository userRepository,
    //        INotificationService notificationService)
    //    {
    //        _blogPostRepository = blogPostRepository;
    //        _userRepository = userRepository;
    //        _notificationService = notificationService;
    //    }

    //    [HttpGet]
    //    [AllowAnonymous]
    //    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    //    {
    //        var posts = await _blogPostRepository.GetAllAsync();
    //        var paginatedPosts = posts.Skip((page - 1) * pageSize).Take(pageSize);
    //        return Ok(paginatedPosts);
    //    }

    //    [HttpGet("{id}")]
    //    [AllowAnonymous]
    //    public async Task<IActionResult> GetById(int id)
    //    {
    //        var post = await _blogPostRepository.GetByIdAsync(id);
    //        if (post == null) return NotFound();
    //        return Ok(post);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Create([FromBody] CreateBlogPostDto postDto)
    //    {
    //        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    //        var user = await _userRepository.GetByIdAsync(userId);

    //        var post = new BlogPost
    //        {
    //            Title = postDto.Title,
    //            Content = postDto.Content,
    //            CreatedAt = DateTime.UtcNow,
    //            UserId = userId,
    //            Author = user
    //        };

    //        await _blogPostRepository.AddAsync(post);

    //        // Add tags
    //        foreach (var tagName in postDto.Tags)
    //        {
    //            await _blogPostRepository.AddTagToPostAsync(post.Id, tagName);
    //        }

    //        return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
    //    }

    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> Update(int id, [FromBody] UpdateBlogPostDto postDto)
    //    {
    //        var post = await _blogPostRepository.GetByIdAsync(id);
    //        if (post == null) return NotFound();

    //        // Check if the user owns the post
    //        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    //        if (post.UserId != userId) return Forbid();

    //        post.Title = postDto.Title;
    //        post.Content = postDto.Content;
    //        post.UpdatedAt = DateTime.UtcNow;

    //        // Update tags
    //        var currentTags = post.Tags.Select(t => t.Name).ToList();
    //        var tagsToAdd = postDto.Tags.Except(currentTags);
    //        var tagsToRemove = currentTags.Except(postDto.Tags);

    //        foreach (var tagName in tagsToAdd)
    //        {
    //            await _blogPostRepository.AddTagToPostAsync(post.Id, tagName);
    //        }

    //        foreach (var tagName in tagsToRemove)
    //        {
    //            var tag = post.Tags.FirstOrDefault(t => t.Name == tagName);
    //            if (tag != null)
    //            {
    //                await _blogPostRepository.RemoveTagFromPostAsync(post.Id, tag.Id);
    //            }
    //        }

    //        await _blogPostRepository.UpdateAsync(post);
    //        return NoContent();
    //    }

    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> Delete(int id)
    //    {
    //        var post = await _blogPostRepository.GetByIdAsync(id);
    //        if (post == null) return NotFound();

    //        // Check if the user owns the post
    //        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    //        if (post.UserId != userId) return Forbid();

    //        await _blogPostRepository.DeleteAsync(id);
    //        return NoContent();
    //    }

    //    [HttpPost("{id}/like")]
    //    public async Task<IActionResult> LikePost(int id)
    //    {
    //        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    //        var alreadyLiked = await _blogPostRepository.UserLikedPostAsync(userId, id);

    //        if (alreadyLiked)
    //        {
    //            return BadRequest("You already liked this post");
    //        }

    //        var like = new Like
    //        {
    //            BlogPostId = id,
    //            UserId = userId
    //        };

    //        await _blogPostRepository.AddLikeAsync(like);

    //        // Send notification to post author
    //        var post = await _blogPostRepository.GetByIdAsync(id);
    //        if (post.UserId != userId) // Don't notify if liking own post
    //        {
    //            await _notificationService.CreateNotificationAsync(
    //                post.UserId,
    //                $"{User.FindFirstValue("displayName")} liked your post: {post.Title}");
    //        }

    //        return Ok();
    //    }

    //    [HttpPost("{id}/unlike")]
    //    public async Task<IActionResult> UnlikePost(int id)
    //    {
    //        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    //        var like = await _blogPostRepository.GetLikeAsync(userId, id);

    //        if (like == null)
    //        {
    //            return BadRequest("You haven't liked this post");
    //        }

    //        await _blogPostRepository.RemoveLikeAsync(like);
    //        return Ok();
    //    }
    //}
}


