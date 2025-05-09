using BlogPlatform.Core;
using BlogPlatform.Core.Dtos;
using BlogPlatform.Core.Models;
using BlogPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Infrastructure.Repositories;

public class CommentRepository
{
    private readonly BlogContext _context;

    public CommentRepository(BlogContext context)
    {
        _context = context;
    }

    public async Task<List<CommentResponseDto>> GetByPostIdAsync(int postId)
    {
        return await _context.Comments
            .Where(c => c.BlogPostId == postId)
            .Include(c => c.Author)
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new CommentResponseDto(
                (int)c.Id,
                c.Content,
                (DateTime)c.CreatedAt,
                c.Author.DisplayName))
            .ToListAsync();
    }

    public async Task<CommentResponseDto> AddAsync(CreateCommentDto commentDto, int postId, string userId)
    {
        var comment = new Comment
        {
            Content = commentDto.Content,
            BlogPostId = postId,
            UserId = userId
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return await _context.Comments
            .Where(c => c.Id == comment.Id)
            .Include(c => c.Author)
            .Select(c => new CommentResponseDto(
                (int)c.Id,
                c.Content,
                (DateTime)c.CreatedAt,
                c.Author.DisplayName))
            .FirstAsync();
    }
}