using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddbookRl
    {
        public AddBookModel AddBook(AddBookModel addBookModel);
        public UpdateBookModel UpdateBook(UpdateBookModel UpdateBookModel);

        public string DeleteBook(int BookId);

        public AddBookModel GetBook(int BookId);

        public List<GetAllBookModel> GetAllBooks();


    }
}
