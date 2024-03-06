CREATE DATABASE BackendTZ;
GO

USE BackendTZ;
GO

CREATE TABLE [Key] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE [Value] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
	Probability FLOAT NOT NULL,
	KeyId INT NOT NULL,
	FOREIGN KEY (KeyId) REFERENCES [Key](Id)
);
GO

CREATE TABLE Experiment (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DeviceToken nvarchar(255) NOT NULL,
	KeyId INT NOT NULL,
	ValueId INT NOT NULL,
	FOREIGN KEY (KeyId) REFERENCES [Key](Id),
	FOREIGN KEY (ValueId) REFERENCES [Value](Id)
);
GO

INSERT INTO [Key] ([Name])
VALUES ('button_color'),
('price');
GO

INSERT INTO [Value]  ([Name], [KeyId], [Probability])
VALUES ('#FF0000', 1, 33.3),
('#00FF00', 1, 33.3),
('#0000FF', 1, 33.3),
('10', 2, 75),
('20', 2, 10),
('50', 2, 5),
('5', 2, 10);

