/****** Script for SelectTopNRows command from SSMS  ******/
Create table Users
( UserId int identity(1,1) Primary key,
FullName Varchar(225) not null,
Email Varchar(225) not null unique,
Password varchar(225) not null,
MobileNumber bigint not null
)

  select *from Users 

  Create procedure SP_User_Registration
(
@FullName varchar(255),
@Email varchar(255),
@Password Varchar(255),
@MobileNumber Bigint
)
as
Begin
		insert Users
		values (@FullName, @Email, @Password, @MobileNumber) 
end


Create procedure spLogin
(
@Email varchar(250),
@Password varchar(250)

)
as
Begin
		Select * from Users where Email = @Email and Password =@Password 
end;
