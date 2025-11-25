CREATE TABLE TimeLogs(
	TimeLogID INT PRIMARY KEY not null,
	LogDate DATETIME not null,
	TimeSpent TIME(0),
	FKProjectID INT,
	FKActivityID INT not null CONSTRAINT Fk_Timelogs_Acitvity FOREIGN KEY(FKActivityID) REFERENCES Activity(ActivityID),
	FKTeamMemberID INT
);

----TODO: Add these after Project and TeamMember is in the database
--ALTER TABLE TimeLogs
--ADD CONSTRAINT Fk_TimeLogs_Project
--FOREIGN KEY(FKProjectID) REFERENCES Project(ProjectID);

--ALTER TABLE TimeLogs
--ADD CONSTRAINT Fk_TimeLogs_TeamMember
--FOREIGN KEY(FKTeamMemberID) REFERENCES TeamMember(TeamMemberID);

----If no constraint is on FKActivity run this
--ALTER TABLE TimeLogs
--ADD CONSTRAINT Fk_Timelogs_Acitvity
--FOREIGN KEY(FKActivity) REFERENCES Activity(ActivityID);