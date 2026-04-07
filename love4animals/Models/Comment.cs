namespace love4animals.Models;

public class Comment
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    // Relaciones
    public Guid PostId { get; set; } 
    public Guid UserId { get; set; } // Quien hizo el comentario
}