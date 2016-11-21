--Create database Booking

Create table Users(
ID int identity primary key,
FirstName varchar(24) not null,
LastName varchar(24) not null,
Email varchar(64) unique not null,
Category varchar(44) not null,
)