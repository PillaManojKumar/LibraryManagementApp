Create database LibraryManagementDB


Create Table Users 
(
UserId Int Primary Key,
Username Varchar(50),
Password Varchar(50)
)

Insert Into Users (UserId, Username, Password)
Values (1, 'user1', 'password1'),
       (2, 'user2', 'password2'),
       (3, 'user3', 'password3');
    

Create Table Books 
(
BookId Int Identity(1,1) Primary Key,
Title Varchar(100),
Author Varchar(100),
Publication Varchar(100),
IsAvailable BIT
)

Create Table Students 
(
StudentId Int Identity(1,1) Primary Key,
Name Varchar(100),
Phonenumber bigint
)
drop table Students

Create Table IssuedBooks 
(
IssueId Int Identity(1,1) Primary Key,
StudentId Int,
BookId Int,
IssueDate Date,
ReturnDate Date,
Foreign Key (StudentId) References Students(StudentId),
Foreign Key (BookId) References Books(BookId)
)

drop table IssuedBooks

select * from Students
select * from Books
select * from IssuedBooks 
select * from Users

