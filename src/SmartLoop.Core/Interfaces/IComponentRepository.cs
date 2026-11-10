using SmartLoop.Core.Models;

namespace SmartLoop.Core.Interfaces;

public interface IComponentRepository
{
    Task<IReadOnlyList<Component>> GetAllAsync();
    Task<Component?> GetByIdAsync(int id);
    Task<int> AddAsync(Component component);
    Task UpdateAsync(Component component);
    Task DeleteAsync(int id);
}