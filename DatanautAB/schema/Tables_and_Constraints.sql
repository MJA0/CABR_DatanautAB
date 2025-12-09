
-- Tabell för projektstatus
-- Säkerställer att varje status har ett unikt namn, alltså inga dubbletter
CREATE TABLE dbo.ProjectStatus(
	StatusID INT IDENTITY(1,1) PRIMARY KEY,
	StatusName NVARCHAR(100) NOT NULL,
	CONSTRAINT UQ_ProjectStatus_StatusName UNIQUE (StatusName) 
);
GO

-- Tabell för vilken roll varje individ har
-- Säkerställer inga dubbletter av rollnamn
CREATE TABLE dbo.MemberRole(
	RoleID INT IDENTITY(1,1) PRIMARY KEY,
	RoleName NVARCHAR(100) NOT NULL,
	CONSTRAINT UQ_MemberRole_RoleName UNIQUE (RoleName)
);
GO

-- Tabell för vilken skill varje individ har
-- Säkerställer att samma kompetens inte registreras flera gånger
CREATE TABLE dbo.Skill(
	SkillID INT IDENTITY(1,1) PRIMARY KEY,
	SkillName NVARCHAR(100) NOT NULL,
	CONSTRAINT UQ_Skill_SkillName UNIQUE (SkillName)
);
GO

-- Tabell för händelselogg 
-- Säkerställer att samma aktivitet/händelse inte registreras flera gånger
CREATE TABLE dbo.Activity(
	ActivityID INT IDENTITY(1,1) PRIMARY KEY,
	ActivityName NVARCHAR(100) NOT NULL,
	CONSTRAINT UQ_Activity_ActivityName UNIQUE (ActivityName)
);
GO

-- Tabell för utrustning (DF=default,UQ=unique,sätter eget namn istället för autogenererat, lättare vid spårning)
-- Sätter standardvärde om inget anges DEFAULT (0)
-- Förhindrar dubbletter av utrusningsnamnm, serienummer ska vara unikt
-- Bytte ut Condition till EquipmentCondition
CREATE TABLE dbo.Equipment(
	EquipmentID INT IDENTITY(1,1) PRIMARY KEY,
	EquipmentName NVARCHAR(100) NOT NULL,
	EquipmentQuantity INT NOT NULL CONSTRAINT DF_Equipment_Quantity DEFAULT (0),
	SerialNumber NVARCHAR(100) NOT NULL,
	Vendor NVARCHAR(100) NULL,
	EquipmentCondition NVARCHAR(100) NULL,
	Cost DECIMAL(10,2) NULL,
	CONSTRAINT UQ_Equipment_EquipmentName UNIQUE (EquipmentName),
	CONSTRAINT UQ_Equipment_SerialNumber UNIQUE (SerialNumber)
);
GO

-- Tabell för licenser
-- Förhindrar dubbletter av licensnamn, licensnycklar måste vara unika
-- Sätter standardvärde om inget anges DEFAULT (0)
CREATE TABLE dbo.License(
	LicenseID INT IDENTITY(1,1) PRIMARY KEY,
	LicenseName NVARCHAR(100) NOT NULL,
	LicenseKey NVARCHAR(100) NOT NULL,
	LicenseQuantity INT NOT NULL CONSTRAINT DF_License_Quantity DEFAULT (0),
	LicenseVersion NVARCHAR(100) NULL,
	Vendor NVARCHAR(100) NULL,
	ExpirationDate DATE NULL,
	Cost DECIMAL(10,2) NULL,
	CONSTRAINT UQ_License_LicenseName UNIQUE (LicenseName),
	CONSTRAINT UQ_License_LicenseKey UNIQUE (LicenseKey)
);
GO

-- Tabell för programvara
-- Säkerställer att programvara inte registreras flera gånger
-- Sätter standardvärde om inget anges DEFAULT (0)
-- BIT typ accepterar 0 (false) ,1 (true) och NULL (som en bool) 
CREATE TABLE dbo.Software(
	SoftwareID INT IDENTITY(1,1) PRIMARY KEY,
	SoftwareName NVARCHAR(100) NOT NULL,
	SoftwareQuantity INT NOT NULL CONSTRAINT DF_Software_Quantity DEFAULT (0),
	SoftwareVersion NVARCHAR(100) NULL,
	Vendor NVARCHAR(100) NULL,
	Operationsystem NVARCHAR(100) NULL,
	RequireLicense BIT NOT NULL CONSTRAINT DF_Software_RequireLicense DEFAULT (0),
	Cost DECIMAL(10,2) NULL,
	CONSTRAINT UQ_Software_SoftwareName UNIQUE (SoftwareName)
);
GO

-- Tabell för teammedlem
-- Säkerställer att en medlem alltid har en giltig roll
-- Säkerställer att medlemmen har en giltig kompetens
-- Förhindrar dubbletter av email
CREATE TABLE TeamMember(
	TeamMemberID INT IDENTITY(1,1) PRIMARY KEY,
	FKMemberRoleID INT NOT NULL,
	FKSkillID INT NOT NULL,
	FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	CONSTRAINT FK_TeamMember_MemberRole FOREIGN KEY (FKMemberRoleID)
		REFERENCES MemberRole(RoleID),
	CONSTRAINT FK_TeamMember_Skill FOREIGN KEY (FKSkillID)
		REFERENCES Skill(SkillID),
	CONSTRAINT UQ_TeamMember_Email UNIQUE (Email)
);
GO

-- Tabell för projekt
-- Säkerställer att projekt alltid har en giltig status
-- Projekt måste ha en giltig projektledare
-- Förhindrar dubbletter av projektnamn
CREATE TABLE Project(
	ProjectID INT IDENTITY(1,1) PRIMARY KEY,
	FKProjectStatusID INT NOT NULL,
	FKProjectManagerID INT NOT NULL,
	ProjectName NVARCHAR(100) NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NULL,
	Budget DECIMAL(10,2) NULL,
	CONSTRAINT FK_Project_Manager FOREIGN KEY (FKProjectManagerID)
		REFERENCES TeamMember(TeamMemberID),
	CONSTRAINT FK_ProjectStatus FOREIGN KEY (FKProjectStatusID)
		REFERENCES ProjectStatus(StatusID),
	CONSTRAINT UQ_Project_ProjectName UNIQUE (ProjectName)
);
GO

-- Tabell för resurser (kopplingstabell)
-- Resurser måste hopplas till ett giltigt projekt
-- ALla tre resterande FK's är nullade eftersom en resurs kan vara vilken typ som helst, men måste alltid ha ett projekt
CREATE TABLE ProjectResource(
	ResourceID INT IDENTITY(1,1) PRIMARY KEY,
	FKProjectID INT NOT NULL,
	FKLicenseID INT NULL,
	FKSoftwareID INT NULL,
	FKEquipmentID INT NULL,
	CONSTRAINT FK_Resource_Project FOREIGN KEY (FKProjectID)
        REFERENCES Project(ProjectID),
    CONSTRAINT FK_Resource_License FOREIGN KEY (FKLicenseID)
        REFERENCES License(LicenseID),
    CONSTRAINT FK_Resource_Software FOREIGN KEY (FKSoftwareID)
        REFERENCES Software(SoftwareID),
    CONSTRAINT FK_Resource_Equipment FOREIGN KEY (FKEquipmentID)
        REFERENCES Equipment(EquipmentID)
);
GO

-- Tabell för projektteam (kopplingstabell)
-- Säkerställer att teamet tillhör ett giltigt projekt
-- Säkerställer att teammedlemmen finns
CREATE TABLE ProjectTeam(
	ProjectTeamID INT IDENTITY(1,1) PRIMARY KEY,
	FKProjectID INT NOT NULL,
	FKTeamMemberID INT NOT NULL,
	CONSTRAINT FK_ProjectTeam_Project FOREIGN KEY (FKProjectID)
        REFERENCES Project(ProjectID),
    CONSTRAINT FK_ProjectTeam_TeamMember FOREIGN KEY (FKTeamMemberID)
		REFERENCES TeamMember(TeamMemberID)
);
GO

-- Tabell för tidloggar
-- Tidsloggen måste kopplas till ett giltigt projekt
-- Tidsloggen måste ha en gilig aktivitet
-- Tidsloggen måste kopplas till en befintlig medlem
CREATE TABLE TimeLogs(
	TimeLogID INT IDENTITY(1,1) PRIMARY KEY,
	FKProjectID INT NOT NULL,
	FKActivityID INT NOT NULL,
	FKTeamMemberID INT NOT NULL,
	LogDate DATETIME NOT NULL,
	TimeSpent TIME NOT NULL,
	CONSTRAINT FK_TimeLogs_Project FOREIGN KEY (FKProjectID)
        REFERENCES Project(ProjectID),
    CONSTRAINT FK_TimeLogs_Activity FOREIGN KEY (FKActivityID)
        REFERENCES Activity(ActivityID),
    CONSTRAINT FK_TimeLogs_TeamMember FOREIGN KEY (FKTeamMemberID)
        REFERENCES TeamMember(TeamMemberID)
);