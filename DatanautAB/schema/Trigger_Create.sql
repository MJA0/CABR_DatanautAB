-- ProjectTeam: Check duplicate team member after insert in Project Team
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
            ON pt.FKTeamMemberID = i.FKTeamMemberID
            AND pt.FkProjectId = i.FkProjectId
        WHERE pt.ProjectTeamID <> i.ProjectTeamID
    )
    BEGIN
        RAISERROR('Trying to insert uplicate team member in project', 16, 1);
        RETURN;
    END
END;

GO

-- ProjectTeam: Check overlapping skills or roles in the same project during after insert in Project Team
CREATE TRIGGER check_overlapping_skill_role
ON ProjectTeam
AFTER INSERT
AS
BEGIN

    IF EXISTS (
        SELECT 1
        FROM INSERTED i
        JOIN ProjectTeam pt 
            ON pt.FkProjectId = i.FkProjectId
            AND pt.ProjectTeamID <> i.ProjectTeamID 
        JOIN TeamMember tm1 
            ON tm1.TeamMemberID = pt.FKTeamMemberID
        JOIN TeamMember tm2 
            ON tm2.TeamMemberID = i.FKTeamMemberID
        WHERE tm1.FkSkillID = tm2.FkSkillID
           OR tm1.FKMemberRoleID  = tm2.FKMemberRoleID
    )
    BEGIN
        PRINT('Overlapping skill or role!');

        -- Add if insert shoul be cancelled
        --RAISERROR('Cannot insert: skill or role already assigned to this project.', 16, 1);
        --ROLLBACK TRANSACTION;
        --RETURN;
    END
END;

GO

-- Resourse: Updates quantity when assigning a software in resourse, if quantity is less than 0 cancel insert
CREATE TRIGGER software_quantity_update
ON ProjectResource
AFTER INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN Software s ON i.FKSoftwareID = s.SoftwareId
        WHERE s.SoftwareQuantity <= 0
    )
    BEGIN
        RAISERROR('Cannot insert Resource: Software quantity is 0.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Else
    UPDATE s
    SET s.SoftwareQuantity = s.SoftwareQuantity - 1
    FROM Software s
    INNER JOIN inserted i ON s.SoftwareId = i.FKSoftwareID;
END;
