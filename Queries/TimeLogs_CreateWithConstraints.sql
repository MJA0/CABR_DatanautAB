CREATE TABLE TimeLogs(
	TimeLogID INT PRIMARY KEY not null,
	LogDate DATETIME not null,
	TimeSpent TIME(0),
	FKProjectID INT not null CONSTRAINT Fk_Timelogs_Project FOREIGN KEY(FKProjectID) REFERENCES Project(ProjectID),
	FKActivityID INT not null CONSTRAINT Fk_Timelogs_Acitvity FOREIGN KEY(FKActivityID) REFERENCES Activity(ActivityID),
	FKTeamMemberID INT not null CONSTRAINT Fk_Timelogs_TeamMember FOREIGN KEY(FKTeamMemberID) REFERENCES TeamMember(TeamMemberID)
);