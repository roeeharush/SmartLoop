USE SmartLoop;
GO

CREATE TABLE Components (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Name        NVARCHAR(100)  NOT NULL,
    Type        NVARCHAR(20)   NOT NULL
                CHECK (Type IN ('Sensor', 'Controller', 'Actuator')),
    Unit        NVARCHAR(20)   NOT NULL,
    MinValue    DECIMAL(12,4)  NOT NULL,
    MaxValue    DECIMAL(12,4)  NOT NULL,
    Description NVARCHAR(500)  NULL,
    CreatedAt   DATETIME2      NOT NULL DEFAULT GETDATE(),
    CONSTRAINT CK_Components_Range CHECK (MinValue < MaxValue)
);
GO

CREATE TABLE ComponentParameters (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    ComponentId INT            NOT NULL,
    Name        NVARCHAR(50)   NOT NULL,
    Value       DECIMAL(12,4)  NOT NULL,
    CONSTRAINT FK_ComponentParameters_Components
        FOREIGN KEY (ComponentId) REFERENCES Components(Id)
        ON DELETE CASCADE,
    CONSTRAINT UQ_ComponentParameters_Component_Name
        UNIQUE (ComponentId, Name)
);
GO

CREATE TABLE ControlSystems (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Name        NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    CreatedAt   DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE SystemComponents (
    SystemId    INT          NOT NULL,
    ComponentId INT          NOT NULL,
    Role        NVARCHAR(20) NOT NULL
                CHECK (Role IN ('Sensor', 'Controller', 'Actuator')),
    Position    INT          NOT NULL,
    CONSTRAINT PK_SystemComponents
        PRIMARY KEY (SystemId, ComponentId, Role),
    CONSTRAINT FK_SystemComponents_ControlSystems
        FOREIGN KEY (SystemId) REFERENCES ControlSystems(Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_SystemComponents_Components
        FOREIGN KEY (ComponentId) REFERENCES Components(Id)
);
GO