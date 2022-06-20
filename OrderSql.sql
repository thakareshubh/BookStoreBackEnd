use Bookstore

create table OrderTables
(
	OrderId int identity(1,1) not null primary key,
	TotalPrice int not null,
	BookQuantity int not null,
	OrderDate Date not null,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
	BookId INT NOT NULL FOREIGN KEY REFERENCES Book(BookId),
	AddressId int not null FOREIGN KEY REFERENCES AddressTable(AddressId)
);
select * from OrderTables;

-----add strore proc for order-----


Create Proc AddOrder
(
	@BookQuantity int,
	@UserId int,
	@BookId int,
	@AddressId int
)
as
Declare @TotalPrice int
BEGIN
	set @TotalPrice = (select DiscountPrice from Book where BookId = @BookId);
	
			If(Exists (Select * from Book where BookId = @BookId))
				BEGIN
					Begin try
						Begin Transaction
						Insert Into OrderTables(TotalPrice, BookQuantity, OrderDate, UserId, BookId, AddressId)
						Values(@TotalPrice*@BookQuantity, @BookQuantity, GETDATE(), @UserId, @BookId, @AddressId);
						Update Book set @BookQuantity= @BookQuantity-@BookQuantity where BookId = @BookId;
						Delete from Cart where BookId = @BookId and UserId = @UserId;
						select * from OrderTables;
						commit Transaction
					End try
					Begin Catch
							rollback;
					End Catch
				END
			
	Else
		Begin
			Select 2;
		End
END;

Create Proc GetOrders
(
	@UserId int
)
as
begin
		Select 
		OrderTables.OrderId, OrderTables.UserId, OrderTables.AddressId, Book.BookId,
		OrderTables.TotalPrice, OrderTables.BookQuantity, OrderTables.OrderDate,
		Book.BookName, Book.AuthorName, Book.BookImage
		FROM Book 
		inner join OrderTables on OrderTables.BookId = Book.BookId 
		where 
			OrderTables.UserId = @UserId;
END