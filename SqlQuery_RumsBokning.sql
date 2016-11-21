--Create database Booking

--Create table aaa.Users(
--ID nvarchar(450) primary key,
--FirstName varchar(24) not null,
--LastName varchar(24) not null,
--Email nvarchar(256) unique not null,
--Category varchar(44) not null,
--constraint fk_usersID foreign key (ID) references AspNetUsers (Id)
--) 
--go
--create schema aaa

--select * from aaa.Room
--go
--sp_help [aaa.Room]


--Create Table aaa.RoomTime
--(
--	Id int Unique not null,
--	StartTime dateTime, 
--	EndTime dateTime, 
--	constraint fk_RoomId foreign key(Id) references aaa.Room(Id)
--)

--Create Table aaa.RoomUsers
--(
--	Id int unique not null,
--	R_Id int not null,
--	U_Id nVarChar(450) not null,
--	constraint fk_RoomUsersId foreign key(R_Id) references aaa.Room(Id),
--	constraint fk_PersonId foreign key(U_Id) references aaa.Users(Id)
--)

