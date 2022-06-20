use Bookstore

----creating table for cart---

Create Table Cart
(
CartId int identity(1,1) primary key,
Book_Quantity int default 1,
UserId int not null foreign key (UserId) references Users(UserId),
BookId int not null Foreign key (BookId) references Book(BookId)
)


select  *  From Cart

---Procedure for add cart----

Create proc spAddCart
( @BookQuantity int,
@UserId int,
@BookId int
)
As
Begin
	insert into cart(Book_Quantity,UserId,BookId)
	values ( @BookQuantity,@UserId, @BookId);
End

----Create procedure for ----
Create proc spRemoveCart
(
@CartId int
)
As
Begin
	delete from Cart where CartId = @CartId;
end

---Procedure for get all cart---
alter proc spGetAllCart
(
@UserId int
)
AS
Begin
	select
		CartId,
		c.BookId,
		UserId,
		BookQuantity,
		BookName,
		BookImage,
		AuthorName,
		DiscountPrice,
		ActualPrice
		from Cart c
		join Book b
		on c.BookId = b.BookId
		where UserId = @UserId;
end

----update quantity---

alter proc UpdateCart
(
	@BookQuantity int,
	@BookId int,
	@UserId int,
	@CartId int
)
as
begin
update Cart set BookId=@BookId,
				UserId=@UserId,
				Book_Quantity=@BookQuantity
				where CartId=@CartId;
end;