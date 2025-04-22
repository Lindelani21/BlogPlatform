using BlogPlatform.Core.Dtos;
using BlogPlatform.Core.Models;
using BlogPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Infrastructure.Repositories
{
    public class LikeRepository
    {
        private readonly BlogContext _context;

        public LikeRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<LikeResponseDto> GetLikeStatus(int postId, string? userId)
        {
            var totalLikes = await _context.Likes
                .CountAsync(l => l.BlogPostId == postId);

            var isLiked = userId != null &&
                await _context.Likes
                    .AnyAsync(l => l.BlogPostId == postId && l.UserId == userId);

            return new LikeResponseDto(totalLikes, isLiked);
        }

        public async Task ToggleLike(int postId, string userId)
        {
            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(l => l.BlogPostId == postId && l.UserId == userId);

            if (existingLike != null)
            {
                _context.Likes.Remove(existingLike);
            }
            else
            {
                _context.Likes.Add(new Like
                {
                    BlogPostId = postId,
                    UserId = userId
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}





