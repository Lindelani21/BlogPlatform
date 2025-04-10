using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Core.Dtos;

public record AuthRequest(string Email, string Password);