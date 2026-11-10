namespace SmartLoop.Core.Models;

public class ComponentParameter
{
    public int Id { get; set; }
    public int ComponentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
}