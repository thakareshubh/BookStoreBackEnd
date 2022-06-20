using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeadBackController : ControllerBase
    {
        private readonly IfeadbackBl ifeadbackBl;

        public FeadBackController(IfeadbackBl ifeadbackBl)
        {
            this.ifeadbackBl = ifeadbackBl;
        }


        //[Authorize(Roles = Role.User)]
        [HttpPost("AddFeedback")]
        public ActionResult AddFeedback(FeedBackModel feedback)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.ifeadbackBl.AddFeedback( feedback,  userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Add feadback  successfull", Response = result });

                }
                return this.BadRequest(new { Success = true, message = " adding Feadback failed", Response = result });

            }
            catch
            {

                throw;
            }
        }

        //[Authorize(Roles = Role.User)]
        [HttpGet("GetRecordsByBookId/{bookId}")]
        public ActionResult GetRecordsByBookId(int bookId)
        {
            try
            {
               
                var result = this.ifeadbackBl.GetRecordsByBookId( bookId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Add feadback  successfull", Response = result });

                }
                return this.BadRequest(new { Success = true, message = " adding Feadback failed", Response = result });

            }
            catch
            {

                throw;
            }
        }
    }
}
