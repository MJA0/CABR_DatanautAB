CREATE TABLE Equipment (
	EquipmentID INT IDENTITY(1,1) PRIMARY KEY,
	EquipmentName NVARCHAR(100) NOT NULL,
	EquipmentQuantity INT NOT NULL,
	SerialNumber NVARCHAR(100) NULL,
	Vendor NVARCHAR(100) NULL,
	Condition NVARCHAR(100) NULL,
	Cost Decimal (10,2) NULL,
);