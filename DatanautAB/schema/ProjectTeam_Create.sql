CREATE TABLE ProjectTeam(
	ProjectTeamID INT PRIMARY KEY not null,
	FkProjectId INT,
	FkTeamMember INT
);

-- TODO: Alter after Project is in the database
--ALTER TABLE ProjectTeam
--ADD CONSTRAINT Fk_ProjectTeam_Project
--FOREIGN KEY(FKProjectID) REFERENCES Project(ProjectID);

--ALTER TABLE ProjectTeam
--ADD CONSTRAINT Fk_ProjectTeam_TeamMember
--FOREIGN KEY(FKProjectID) REFERENCES TeamMember(TeamMemberID);