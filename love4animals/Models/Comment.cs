namespace love4animals.Models;

public class Comment
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    // Relaciones
    public Guid PostId { get; set; } 
    public Guid UserId { get; set; } // Quien hizo el comentario
    public Post Post { get; set; } = null!; // Un comentario pertenece a un post

    public User User { get; set; } = null!; // Un comentario tiene un usuario que lo hizo

    
}