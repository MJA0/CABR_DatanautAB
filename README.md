# DatanautAB

Datanaut AB is a technology company specializing in the development of digital tools for project management in space-related research initiatives and advanced engineering projects.
The company collaborates with both private sector partners and public research institutions.
To streamline internal project management, Datanaut is developing a new database solution that will serve as the foundation for a future web-based platform.

### Participants
- Niklas (reporter)
- Adchariya

### Sprintgoals
- Roles & session goals: Appoint a reporter, a code responsible, and a merge responsible. Write a requirements list and create an initial sketch of the ER diagram.
- Interpret the assignment and write a requirements list
- Identify entities and relationships
- Create an initial sketch of the ER diagram
- Write a backlog and break it down into tasks
### Technologys
- SQL
- C#
- Visual studio
- SQL server management studio (SSMS)
- Entity Framework

### Design
The project design is based on the ER diagram created in ERDplus. We used a "Database First" approach with SQL Server Management Studio (SSMS)
to create the database, and then implemented data exchange using Entity Framework for sending and receiving data.

<img width="5526" height="2580" alt="Draft_3" src="https://github.com/user-attachments/assets/f89b08b9-7e64-4ffd-988a-0505c9f72b79" />


### Problems
- Not enough knowledge about ssms or sql to fully break down storys to tasks


# week 2 (18/11/25)

### Participants
- Niklas
- Adchariya (reporter)
- Robin

### Sprintgoals
- Create a complete ER-diagram
- Normalize ER-diagram to 3NF
- Write backlog with user stories and break down into tasks

### What we did
- Refactored last draft of ER-diagram to current draft, normalised to 3NF
- Broke down user story into tasks
- Each participants tested connecting to github, cloning project and syncing to repository in SSMS

### Problems
- Unsure about team workflow of SQL with github
  

# week 3 (25/11/25)

### Participants
- Niklas
- Adchariya
- Robin (reporter)

### Sprintgoals
- Create the datebase in SSMS
- Create the first tables and relations

### What we did
- Based on our ER-diagram, we created our tables and relations within SSMS
- We separate the work due to initially having a total of 12 entities, so everyone did 4 each to make it even.

### Problems
- Still unsure of how everything works within SSMS
- Time that should be put into working on the project is put to tackle problems we encounter due to our experience


# week 4 (02/11/25)

### Participants
- Niklas (reporter)
- Adchariya
- Robin

### Sprintgoals
- Create trigger to log changes in critical areas
- Add test data to test trigger
- Add foreign key constraints to ensure reltionintegrity
- Add NOT NULL constraints where data is required
- Add Primary key constraints on all relevant tables

### What we did
- Added Trigger to check for dublicated members on project
- Added test data to test trigger
- Update and add attributes to ER diagram

### Problems
- Experienced problems regarding the connection between SSMS and VS code projects

 
# week 5 (10/11/25)

### Participants
- Niklas 
- Adchariya (reporter)
- Robin

### Sprintgoals
- Implement data access with entity framework, ORM

### What we did
- Verified last weeks trigger implmentations
- Scaffolded database into a C# console app project
- Started on a menu system for the database

### Problems
- None
### Contributors
- Robin Markström
- Niklas Eriksson
- Adchariya Changtam
