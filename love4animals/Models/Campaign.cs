namespace love4animals.Models;

public class Campaign
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal GoalAmount { get; set; }
    public decimal CurrentAmount { get; set; } = 0;
}