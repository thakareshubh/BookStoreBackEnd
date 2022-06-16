using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddBookBl : IAddBookBl
    {
        private readonly IAddbookRl iaddbookRl;

        public AddBookBl(IAddbookRl iaddbookRl)
        {
            this.iaddbookRl = iaddbookRl;
        }


        public AddBookModel AddBook(AddBookModel addBookModel)
        {
            try
            {
                return this.iaddbookRl.AddBook(addBookModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteBook(int BookId)
        {
            try
            {
                return this.iaddbookRl.DeleteBook(BookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetAllBookModel> GetAllBooks()
        {
            try
            {
                return this.iaddbookRl.GetAllBooks();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AddBookModel GetBook(int BookId)
        {
            try
            {
                return this.iaddbookRl.GetBook(BookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UpdateBookModel UpdateBook(UpdateBookModel UpdateBookModel)
        {
            try
            {
                return this.iaddbookRl.UpdateBook(UpdateBookModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
