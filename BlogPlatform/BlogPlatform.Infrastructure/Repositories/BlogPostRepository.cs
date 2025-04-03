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
    // BlogPostRepository.cs
    public class BlogPostRepository
    {
        private readonly BlogContext _context;

        public BlogPostRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            return await _context.Posts
                .Include(p => p.Author)
                .ToListAsync();
        }
    }
}
