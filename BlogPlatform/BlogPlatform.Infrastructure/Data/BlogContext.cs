using System;
using System.Collections.Generic;
using System.Data.Entity;
using BlogPlatform.Core.Models;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Infrastructure.Data
{
    public class BlogContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<BlogPost> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BlogPost>()
                .HasOne(p => p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);
        }
    }
}
