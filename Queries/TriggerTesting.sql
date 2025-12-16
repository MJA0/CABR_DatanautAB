SELECT * FROM Activity;
INSERT INTO Activity
VALUES 
	('Planning'),
	('Developing'),
	('Testing');
GO

SELECT * FROM Skill;
INSERT INTO Skill
VALUES 
	('C#'),
	('SQL'),
	('GIT');
GO

SELECT * FROM MemberRole;
INSERT INTO MemberRole
VALUES 
	('Leader'),
	('Programmer'),
	('Designer');
GO

SELECT * FROM TeamMember;
INSERT INTO TeamMember
VALUES 
	(1, 1, 'Alice', 'Alicesson', 'alice@email.com'),
	(2, 2, 'Bob', 'Bobsson', 'bob@email.com'),
	(3, 3, 'Charlie', 'Charliesson', 'charlie@email.com');
GO

SELECT * FROM ProjectStatus;
INSERT INTO ProjectStatus
VALUES 
	('Active'),
	('Inactive'),
	('Complete');
GO

SELECT * FROM TeamMember;
INSERT INTO TeamMember
VALUES 
	(1, 2, 'Dave', 'Davesson', 'dave@email.com'),
	(2, 3, 'Eve', 'Evesson', 'eve@email.com'),
	(3, 1, 'Frank', 'Franksson', 'frank@email.com');
GO

SELECT * FROM Project;
INSERT INTO Project(FKProjectStatusID, FKProjectManagerID, ProjectName, StartDate, EndDate, Budget )
VALUES 
	(1, 3, 'Project Alice', '2025-01-01', '2026-01-01', 1000001.00),
	(2, 4, 'Project Bob', '2026-02-02', '2027-02-02', 2000002.00),
	(3, 5, 'Project Charlie', '2027-03-03', '2028-03-03', 3000003.00);
GO

-- START Trigger check for check_overlapping_skill_role
SELECT * FROM ProjectTeam;
INSERT INTO ProjectTeam
VALUES 
	(1, 1); -- Alice, Lead, C#
GO

INSERT INTO ProjectTeam
VALUES 
	(1, 6); -- Frank, Design, C#
GO

INSERT INTO ProjectTeam
VALUES 
	(1, 4); -- Dave, Lead, Programmer
GO
-- WORKS
-- END Trigger check for check_overlapping_skill_role

-- START Trigger check for software_quantity_update
SELECT * FROM Software;
INSERT INTO Software(SoftwareName, SoftwareQuantity, RequireLicense)
VALUES 
	('Windows', 2, 001)
GO

SELECT * FROM ProjectResource;
INSERT INTO ProjectResource(FKProjectID, FKSoftwareID)
VALUES 
	(1, 0)
GO

SELECT * FROM ProjectResource;
INSERT INTO ProjectResource(FKProjectID, FKSoftwareID)
VALUES 
	(2, 0)
GO

SELECT * FROM ProjectResource;
INSERT INTO ProjectResource(FKProjectID, FKSoftwareID)
VALUES 
	(3, 0)
GO
-- WORKS
-- END Trigger check for software_quantity_update

-- START Trigger check for equipment_quantity_update
SELECT * FROM Equipment;
INSERT INTO Equipment(EquipmentName, EquipmentQuantity, SerialNumber)
VALUES 
	('Computers', 2, '000-001')
GO

SELECT * FROM ProjectResource;
INSERT INTO ProjectResource(FKProjectID, FKEquipmentID)
VALUES 
	(3, 1)
GO

SELECT * FROM ProjectResource;
INSERT INTO ProjectResource(FKProjectID, FKEquipmentID)
VALUES 
	(2, 1)
GO

SELECT * FROM ProjectResource;
INSERT INTO ProjectResource(FKProjectID, FKEquipmentID)
VALUES 
	(1, 1)
GO
-- WORKS
-- END Trigger check for equipment_quantity_update