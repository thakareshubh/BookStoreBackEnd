using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAddBookBl
    {
        public AddBookModel AddBook(AddBookModel addBookModel);
        public UpdateBookModel UpdateBook(UpdateBookModel UpdateBookModel);
        public string DeleteBook(int BookId);
        public AddBookModel GetBook(int BookId);
        public List<GetAllBookModel> GetAllBooks();

    }
}
