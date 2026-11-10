USE SmartLoop;
GO

INSERT INTO Components (Name, Type, Unit, MinValue, MaxValue, Description)
VALUES
    ('Temperature Sensor', 'Sensor', N'°C', -50, 200, 'Measures temperature'),
    ('Pressure Sensor', 'Sensor', 'bar', 0, 40, 'Measures pressure'),
    ('Flow Sensor', 'Sensor', 'L/min', 0, 500, 'Measures liquid flow'),
    ('Level Sensor', 'Sensor', '%', 0, 100, 'Measures tank level'),
    ('PID Controller', 'Controller', '%', 0, 100, 'Classic PID control loop'),
    ('On/Off Controller', 'Controller', '%', 0, 100, 'Simple two-state control'),
    ('Control Valve', 'Actuator', '%', 0, 100, 'Adjusts flow by opening'),
    ('Circulation Pump', 'Actuator', 'rpm', 0, 3000, 'Moves liquid in the loop'),
    ('Electric Heater', 'Actuator', 'kW', 0, 50, 'Adds heat to the system');
GO

SELECT * FROM Components;
GO

INSERT INTO ControlSystems (Name, Description)
VALUES ('Greenhouse Temperature Control', 'Keeps greenhouse temperature stable');
GO

INSERT INTO SystemComponents (SystemId, ComponentId, Role, Position)
VALUES
    (1, 1, 'Sensor', 1),
    (1, 5, 'Controller', 2),
    (1, 9, 'Actuator', 3);
GO

INSERT INTO ComponentParameters (ComponentId, Name, Value)
VALUES
    (5, 'Kp', 1.2),
    (5, 'Ki', 0.5),
    (5, 'Kd', 0.1);
GO