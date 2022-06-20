using BusinessLayer.Interface;
using CommonLayer.cartModel;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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

        [Authorize(Roles = Role.User)]
        [HttpPost("AddCart")]
        public ActionResult AddtoCart(cartModel cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
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

        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteBook/{cartId}")]
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

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllCartBook")]
        public ActionResult GetAllCart()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
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

        [Authorize(Roles = Role.User)]
        [HttpPut("updateCart/{cartId}")]
        public ActionResult updateCart(int cartId, cartModel cartModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.icartBl.updateCart(cartId, cartModel, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "update cart successfull", Response = result });

                }
                return this.BadRequest(new { Success = true, message = "update cart failed", Response = result });

            }
            catch
            {

                throw;
            }
        }



    }
}
