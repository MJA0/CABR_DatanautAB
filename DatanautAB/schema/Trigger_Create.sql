-- Check duplicate team member after insert in Project Team
CREATE TRIGGER check_teamMember
ON ProjectTeam
AFTER INSERT
AS 
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (
        SELECT 1
        FROM ProjectTeam pt
        INNER JOIN inserted i
            ON pt.FkTeamMember = i.FkTeamMember
            AND pt.FkProjectId = i.FkProjectId
        WHERE pt.ProjectTeamID <> i.ProjectTeamID
    )
    BEGIN
        RAISERROR('Trying to insert uplicate team member in project', 16, 1);
        RETURN;
    END
END;

GO

-- TODO: Fix after skill id has been implemented in TeamMember
-- Check overlapping skills in the same project during after insert in Project Team
--CREATE TRIGGER check_overlapping_skill
--ON ProjectTeam
--AFTER INSERT
--AS 
--BEGIN
--    SET NOCOUNT ON;
--    IF EXISTS (
--        SELECT 1
--        FROM ProjectTeam pt
--        INNER JOIN inserted i
--            ON pt.FkTeamMember = i.FkTeamMember
--            AND pt.FkProjectId = i.FkProjectId
--        WHERE pt.ProjectTeamID <> i.ProjectTeamID
--    )
--    BEGIN
--        PRINT('Warning, team member with skill already in project');
--        RETURN;
--    END
--END;

--GO

-- TODO: Fix after role id has been implemented in TeamMember
-- Check overlapping role in the same project during after insert in Project Team
--CREATE TRIGGER check_overlapping_role
--ON ProjectTeam
--AFTER INSERT
--AS 
--BEGIN
--    SET NOCOUNT ON;
--    IF EXISTS (
--        SELECT 1
--        FROM ProjectTeam pt
--        INNER JOIN inserted i
--            ON pt.FkTeamMember = i.FkTeamMember
--            AND pt.FkProjectId = i.FkProjectId
--        WHERE pt.ProjectTeamID <> i.ProjectTeamID
--    )
--    BEGIN
--        PRINT('Warning, team member with skill already in project');  
--        RETURN;
--    END
--END;