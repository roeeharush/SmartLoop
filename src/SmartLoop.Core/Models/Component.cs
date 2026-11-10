namespace SmartLoop.Core.Models;

public class Component
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ComponentType Type { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ComponentParameter> Parameters { get; set; } = new();
}