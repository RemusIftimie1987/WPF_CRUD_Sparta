drop database if exists SpartaDB
go

create database SpartaDB
go
use SpartaDB

drop table if exists Clients
go
create table Clients
(
ClientID int Not Null Identity Primary Key,
CompanyName nvarchar(50) Null,
ContactName nvarchar(50) Null
)

drop table if exists Spartans
go
create table Spartans
(
SpartanID int Not Null Identity Primary Key,
SpartanName nvarchar(50) Null,
Course nvarchar(50) Null,
IsPlaced BIT
)

drop table if exists Jobs
go
create table Jobs
(
JobID int Not Null Identity Primary Key,
JobTitle nvarchar(50) Null,
City nvarchar(50) Null,
ClientID int Null,
SpartanID int Null,
Foreign Key (ClientID) References Clients (ClientID),
Foreign Key (SpartanID) References Spartans (SpartanID)
)
go

INSERT INTO Clients Values ('Lloyds_Group', 'Smith')
INSERT INTO Clients Values ('Capgemini', 'Jennifer')
INSERT INTO Clients Values ('Tide_Bank', 'Robin')
INSERT INTO Clients Values ('Clarksons', 'Martin')
INSERT INTO Clients Values ('RBS_Division', 'Laura')
INSERT INTO Clients Values ('Three', 'Listov')
INSERT INTO Spartans Values ('Remus Iftimie', 'C#_Devs', 0)
INSERT INTO Spartans Values ('Isaac Baculima', 'C#_Devs', 0)
INSERT INTO Spartans Values ('Ramon Nelson', 'C#_Devs', 0)
INSERT INTO Spartans Values ('Katie Holmes', 'DevOps', 1)
INSERT INTO Spartans Values ('Jim Copperfield', 'DevOps', 0)
INSERT INTO Spartans Values ('Fran Sinatra', 'Testing', 0)
INSERT INTO Spartans Values ('Lauren Watson', 'Testing', 0)
INSERT INTO Spartans Values ('Marina Santini', 'Java_Devs', 1)
INSERT INTO Spartans Values ('Jackie Fong', 'Business', 0)
INSERT INTO Spartans Values ('Mike Tyson', 'C#_Devs', 1)
INSERT INTO Jobs Values ('Software developer', 'London',1, 1)
INSERT INTO Jobs Values ('Web developer', 'London', 2, 2)
INSERT INTO Jobs Values ('Software Tester', 'Glasgow', 3, 3)
INSERT INTO Jobs Values ('Business analyst', 'Dublin', 4, 4)

select * from Jobs

