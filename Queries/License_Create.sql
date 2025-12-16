CREATE TABLE License (
	LicenseID INT IDENTITY(1,1) PRIMARY KEY,
	LicenseName NVARCHAR(100) NOT NULL,
	LicenseKey NVARCHAR(200) NULL,
	LicenseQuantity INT NOT NULL,
	LicenseVersion NVARCHAR(100) NULL,
	Vendor NVARCHAR(100) NULL,
	ExpirationDate DATE NULL,
	Cost Decimal (10,2) NULL
);