using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Core.Dtos;

public record CreateCommentDto(string Content);
public record CommentResponseDto(
    int Id,
    string Content,
    DateTime CreatedAt,
    string AuthorName);