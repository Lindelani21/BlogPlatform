﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Core.Models
{
    public class AuthRequest
    {
        public readonly string? Email;

        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
