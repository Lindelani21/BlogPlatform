using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Core.Dtos;

public record CreatePostDto(
    string Title,
    string Content,
    List<string> Tags);

public record UpdatePostDto(
    string Title,
    string Content,
    List<string> Tags);

public record PostResponseDto(
    int Id,
    string Title,
    string Content,
    DateTime CreatedAt,
    string AuthorName,
    List<string> Tags);
