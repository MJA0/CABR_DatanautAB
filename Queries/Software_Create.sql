CREATE TABLE Software (
	SoftwareID INT IDENTITY(1,1) PRIMARY KEY,
	SoftwareName NVARCHAR(100) NOT NULL,
	SoftwareQuantity INT NOT NULL,
	SoftwareVersion NVARCHAR(100) NULL,
	Vendor NVARCHAR(100) NULL,
	Operationsystem NVARCHAR(100) NULL,
	RequireLicense BIT NOT NULL DEFAULT 0,
	Cost Decimal (10,2) NULL
);