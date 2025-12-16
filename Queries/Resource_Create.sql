CREATE TABLE dbo.Resource (
	ResourceID INT IDENTITY(1,1) PRIMARY KEY,
	FKProjectID INT NOT NULL,
	FKLicenses INT NULL,
	FKSoftwares INT NULL,
	FKEquipment INT NULL,

	CONSTRAINT FK_Resource_License
		FOREIGN KEY (FKLicenses) REFERENCES dbo.License(LicenseID),

	CONSTRAINT FK_Resource_Software
		FOREIGN KEY (FKSoftwares) REFERENCES dbo.Software(SoftwareID),

	CONSTRAINT FK_Resource_Equipment
		FOREIGN KEY (FKEquipment) REFERENCES dbo.Equipment(EquipmentID),

	CONSTRAINT FK_Resource_Project
		FOREIGN KEY (FKProjectID) REFERENCES dbo.Project(ProjectID)
);