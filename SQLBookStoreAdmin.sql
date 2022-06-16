use Bookstore

----Create Admin Table----

create table AdminTable
(
Admin int primary key identity(1,1),
FullName varchar(250),
Email varchar(255),
Password varchar(255),
PhoneNumber Bigint
);

----insert admin---
insert into AdminTable values('Admin','shubhamthakare329@gmail.com','shubh@1234',7028873490);

select * from AdminTable

---create procedure for admin login--


alter procedure LoginAdmin
(
@Email varchar(255),
@Password varchar(255)
)
as
BEGIN
If(Exists(select * from AdminTable where Email = @Email and Password = @Password))
Begin
select Admin, FullName, Email,Password, PhoneNumber from AdminTable;
end
Else
Begin
select 2;
End
END;

