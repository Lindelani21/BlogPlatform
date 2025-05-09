using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Core.Models
{
    public class BlogPost
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Title { get; set; }
        [Required]
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public string? UserId { get; set; }
        public User? Author { get; set; }
        public List<Comment>? Comments { get; set; } = new();
        public List<Like>? Likes { get; set; } = new();
        public List<Tag>? Tags { get; set; } = new();
    }
}
