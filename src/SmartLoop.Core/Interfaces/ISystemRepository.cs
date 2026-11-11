using SmartLoop.Core.Models;

namespace SmartLoop.Core.Interfaces;

public interface ISystemRepository
{
    Task<int> AddAsync(ControlSystem system);
}