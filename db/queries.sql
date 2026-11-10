USE SmartLoop;
GO

-- Query 1: all controllers
SELECT Name, Unit
FROM Components
WHERE Type = 'Controller';
GO

-- Query 2: components with a maximum value above 100
SELECT Name, MaxValue
FROM Components
WHERE MaxValue > 100;
GO

-- Query 3: components of system 1, with their role and position (JOIN)
SELECT Components.Name, SystemComponents.Role, SystemComponents.Position
FROM SystemComponents
JOIN Components ON SystemComponents.ComponentId = Components.Id
WHERE SystemComponents.SystemId = 1
ORDER BY SystemComponents.Position;
GO