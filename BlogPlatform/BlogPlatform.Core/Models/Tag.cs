﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Core.Models
{
    public class Tag
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<BlogPost>? BlogPosts { get; set; }
    }
}
