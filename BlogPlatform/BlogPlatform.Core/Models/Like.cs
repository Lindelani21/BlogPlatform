using BlogPlatform.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Core.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
