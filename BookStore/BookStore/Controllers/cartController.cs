using BusinessLayer.Interface;
using CommonLayer.cartModel;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class cartController : ControllerBase
    {
        private readonly IcartBl icartBl;

        public cartController(IcartBl icartBl)
        {
            this.icartBl = icartBl;
        }


        [HttpPost("AddCart")]
        public ActionResult AddtoCart(cartModel cart, int userId)
        {
            try
            {
                var result = this.icartBl.AddtoCart(cart, userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<cartModel>() { status = true, message = $"cart book Successful", Data = result });

                }
                return this.BadRequest(new ResponseModel<cartModel>() { status = true, message = $" cart Register Failed", Data = result });

            }
            catch
            {

                throw;
            }
        }

        [HttpDelete("DeleteBook")]
        public ActionResult removeCart(int cartId)
        {
            try
            {
                var result = this.icartBl.removeCart(cartId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string>() { status = true, message = $"Delete cart Successful", Data = result });

                }
                return this.BadRequest(new ResponseModel<string>() { status = true, message = $" cart delete Failed", Data = result });

            }
            catch
            {

                throw;
            }
        }

        [HttpGet("GetAllCartBook")]
        public ActionResult GetAllCart(int userId)
        {
            try
            {
                var result = this.icartBl.GetAllCart(userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Get all cart books successfull", Response = result });

                }
                return this.BadRequest(new { Success = false, message = "Get all books failed", Response = result });

            }
            catch
            {

                throw;
            }
        }


    }
}
