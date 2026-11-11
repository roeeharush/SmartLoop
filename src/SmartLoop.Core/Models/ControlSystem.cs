namespace SmartLoop.Core.Models;

public class ControlSystem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<SystemComponent> Components { get; set; } = new();
}
