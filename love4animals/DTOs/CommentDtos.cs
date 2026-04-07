namespace love4animals.DTOs;

public record GetCommentDto(Guid Id, string Text, DateTime CreatedAt, Guid PostId, Guid UserId);
public record CreateCommentDto(string Text, Guid UserId);
public record UpdateCommentDto(string Text);