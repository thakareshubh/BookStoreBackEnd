using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IAddBookBl iaddbookBl;

        public BookController(IAddBookBl iaddbookBl)
        {
            this.iaddbookBl = iaddbookBl;
        }

        [HttpPost("AddBook")]
        public ActionResult AddBook(AddBookModel addBookModel)
        {
            try
            {
                var result = this.iaddbookBl.AddBook(addBookModel);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<AddBookModel>() { status = true, message = $"Added book Successful", Data = result });

                }
                return this.BadRequest(new ResponseModel<AddBookModel>() { status = true, message = $" Book Register Failed", Data = result });

            }
            catch 
            {

                throw ;
            }
        }


        [HttpPut("UpdateBook")]
        public ActionResult UpdateBook(UpdateBookModel UpdateBookModel)
        {
            try
            {
                var result = this.iaddbookBl.UpdateBook(UpdateBookModel);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<UpdateBookModel>() { status = true, message = $"Updated book Successful", Data = result });

                }
                return this.BadRequest(new ResponseModel<UpdateBookModel>() { status = true, message = $" Book Updated Failed", Data = result });

            }
            catch
            {

                throw;
            }
        }


        [HttpGet("GetBook")]
        public ActionResult GetBook(int BookId)
        {
            try
            {
                var result = this.iaddbookBl.GetBook(BookId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<AddBookModel>() { status = true, message = $"Get book Successful", Data = result });

                }
                return this.BadRequest(new ResponseModel<AddBookModel>() { status = true, message = $" Book get Failed", Data = result });

            }
            catch
            {

                throw;
            }
        }


        [HttpDelete("DeleteBook")]
        public ActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = this.iaddbookBl.DeleteBook(BookId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string>() { status = true, message = $"Delete book Successful", Data = result });

                }
                return this.BadRequest(new ResponseModel<string>() { status = true, message = $" Book delete Failed", Data = result });

            }
            catch
            {

                throw;
            }
        }

        [HttpGet("GetAllBook")]
        public ActionResult GetAllBooks()
        {
            try
            {
                var result = this.iaddbookBl.GetAllBooks();
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Get all books successfull", Response = result});

                }
                return this.BadRequest(new { Success = false, message = "Get all  failed " });

            }
            catch
            {

                throw;
            }
        }

    }
}
