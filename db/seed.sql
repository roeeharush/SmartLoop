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