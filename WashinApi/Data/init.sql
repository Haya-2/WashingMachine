-- Buildings table creation
CREATE TABLE IF NOT EXISTS Buildings (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
);

-- Users table creation
CREATE TABLE IF NOT EXISTS Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Surname VARCHAR(255) NOT NULL,
    Name VARCHAR(255) NOT NULL,
    Login VARCHAR(255) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    IsManager BOOLEAN NOT NULL,
    Id_Building INT,
    FOREIGN KEY (Id_Building) REFERENCES Buildings(Id) ON DELETE SET NULL
);

-- Machines table creation
CREATE TABLE IF NOT EXISTS Machines (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Id_Building INT,
    Id_User INT,
    IsWorking BOOLEAN NOT NULL,
    FOREIGN KEY (Id_Building) REFERENCES Buildings(Id) ON DELETE SET NULL,
    FOREIGN KEY (Id_User) REFERENCES Users(Id) ON DELETE SET NULL
);

-- Insertion initial data

-- Insertion building
INSERT INTO Buildings (Name) VALUES ('Bâtiment A');

-- Id of building
SET @buildingI = LAST_INSERT_ID();

-- Insertion manager
INSERT INTO Users (Surname, Name, Login, Password, IsManager, Id_Building)
VALUES ('John', 'Doe', 'manager1', 'password123', TRUE, @buildingId);

-- Insertion of 5 residents
INSERT INTO Users (Surname, Name, Login, Password, IsManager, Id_Building) VALUES
('Alice', 'Dupont', 'alice', 'password123', FALSE, @buildingI),
('Bob', 'Martin', 'bob', 'password123', FALSE, @buildingI),
('Charlie', 'Durand', 'charlie', 'password123', FALSE, @buildingI),
('Diane', 'Petit', 'diane', 'password123', FALSE, @buildingI),
('Eve', 'Bernard', 'eve', 'password123', FALSE, @buildingI);

-- Id of user
SET @userId = LAST_INSERT_ID();

-- Insertion du machine
INSERT INTO Machines (Id_Building,Id_User,IsWorking) 
VALUES (@buildingI, @userId,TRUE)
