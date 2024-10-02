-- Create database
CREATE DATABASE AirportSystem;
GO

-- Use the created database
USE AirportSystem;
GO

-- Create table for GeographyLevel1 
CREATE TABLE GeographyLevel1 (
    GeographyLevel1ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

-- Insert sample data into GeographyLevel1
INSERT INTO GeographyLevel1 (Name) VALUES 
('United Kingdom'),
('Spain'),
('United States'),
('Turkey');

-- Create table for Airports
CREATE TABLE Airport (
    AirportID INT IDENTITY(1,1) PRIMARY KEY,
    IATACode NVARCHAR(4) NOT NULL,
    GeographyLevel1ID INT NOT NULL,
    Type NVARCHAR(200) NOT NULL,
    CONSTRAINT FK_Airport_GeographyLevel1 FOREIGN KEY (GeographyLevel1ID)
    REFERENCES GeographyLevel1(GeographyLevel1ID)
);

-- Insert sample data into Airport
INSERT INTO Airport (IATACode, GeographyLevel1ID, Type) VALUES
('LGW', 1, 'Departure and Arrival'),
('PMI', 2, 'Arrival Only'),
('LAX', 3, 'Arrival Only');

-- Create table for Routes
CREATE TABLE Route (
    RouteID INT IDENTITY(1,1) PRIMARY KEY,
    DepartureAirportID INT NOT NULL,
    ArrivalAirportID INT NOT NULL,
    CONSTRAINT FK_Route_DepartureAirport FOREIGN KEY (DepartureAirportID)
    REFERENCES Airport(AirportID),
    CONSTRAINT FK_Route_ArrivalAirport FOREIGN KEY (ArrivalAirportID)
    REFERENCES Airport(AirportID)
);

-- Insert sample data into Route
INSERT INTO Route (DepartureAirportID, ArrivalAirportID) VALUES 
(1, 2);  -- LGW -> PMI


-- Create table for AirportGroup
CREATE TABLE AirportGroup (
    AirportGroupID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

-- Insert sample data into AirportGroup
INSERT INTO AirportGroup (Name) VALUES 
('Group A'),
('Group B');

-- Create many-to-many relationship between Airport and AirportGroup
CREATE TABLE AirportGroupAirport (
    AirportGroupID INT NOT NULL,
    AirportID INT NOT NULL,
    CONSTRAINT PK_AirportGroupAirport PRIMARY KEY (AirportGroupID, AirportID),
    CONSTRAINT FK_AirportGroupAirport_AirportGroup FOREIGN KEY (AirportGroupID)
    REFERENCES AirportGroup(AirportGroupID),
    CONSTRAINT FK_AirportGroupAirport_Airport FOREIGN KEY (AirportID)
    REFERENCES Airport(AirportID)
);

-- Insert sample data into AirportGroupAirport (associating airports with groups)
INSERT INTO AirportGroupAirport (AirportGroupID, AirportID) VALUES
(1, 1),  -- LGW in Group A
(1, 2),  -- PMI in Group A
(2, 3);  -- LAX in Group B