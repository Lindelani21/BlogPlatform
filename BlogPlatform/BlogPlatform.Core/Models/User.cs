using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Core.Models
{
    public class User 
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public ICollection<BlogPost> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
