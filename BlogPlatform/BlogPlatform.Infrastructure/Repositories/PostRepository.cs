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
    public class PostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<List<PostResponseDto>> GetAllAsync()
        {
            return await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Tags)
                .Select(p => new PostResponseDto(
                    p.Id,
                    p.Title,
                    p.Content,
                    p.CreatedAt,
                    p.Author.DisplayName,
                    p.Tags.Select(t => t.Name).ToList()))
                .ToListAsync();
        }

        public async Task<PostResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Where(p => p.Id == id)
                .Include(p => p.Author)
                .Include(p => p.Tags)
                .Select(p => new PostResponseDto(
                    p.Id,
                    p.Title,
                    p.Content,
                    p.CreatedAt,
                    p.Author.DisplayName,
                    p.Tags.Select(t => t.Name).ToList()))
                .FirstOrDefaultAsync();
        }

        public async Task<PostResponseDto> CreateAsync(CreatePostDto postDto, string userId)
        {
            var post = new BlogPost
            {
                Title = postDto.Title,
                Content = postDto.Content,
                UserId = userId
            };

            // Handle tags
            foreach (var tagName in postDto.Tags.Distinct())
            {
                var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName)
                         ?? new Tag { Name = tagName };
                post.Tags.Add(tag);
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(post.Id);
        }

        public async Task<(List<PostResponseDto> Items, int TotalCount)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Tags)
                .OrderByDescending(p => p.CreatedAt);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PostResponseDto(
                    p.Id,
                    p.Title,
                    p.Content,
                    p.CreatedAt,
                    p.Author.DisplayName,
                    p.Tags.Select(t => t.Name).ToList()))
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
