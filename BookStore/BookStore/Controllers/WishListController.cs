using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.WishList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WishListController : Controller
    {
        private readonly IwishListBl iwishListBl;

        public WishListController(IwishListBl iwishListBl)
        {
            this.iwishListBl = iwishListBl;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddWishList")]
        public ActionResult AddWishList(WishListModel wishlistModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.iwishListBl.AddWishList(wishlistModel, userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<WishListModel>() { status = true, message = $"Added book Successful", Data = result });

                }
                return this.BadRequest(new ResponseModel<WishListModel>() { status = true, message = $" Book Register Failed", Data = result });


            }
            catch 
            {

                throw ;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("RemoveWishList")]
        public ActionResult RemoveWishList(int wishListId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.iwishListBl.RemoveWishList(wishListId, userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string>() { status = true, message = $"delete wishlist Successful", Data = result });

                }
                return this.BadRequest(new ResponseModel<string>() { status = true, message = $" delete wishlist Failed", Data = result });


            }
            catch
            {

                throw;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetWishlistDetailsByUserid")]
        public ActionResult GetWishlistDetailsByUserid()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.iwishListBl.GetWishlistDetailsByUserid(userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Get all wishlist books successfull", Response = result });

                }
                return this.BadRequest(new { Success = true, message = "Get all wishlist failed", Response = result });


            }
            catch
            {

                throw;
            }
        }
    }
}
