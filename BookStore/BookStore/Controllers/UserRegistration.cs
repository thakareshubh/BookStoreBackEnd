using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserRegistration : ControllerBase
    {
       private readonly IuserBl iuserBl;

        public UserRegistration(IuserBl iuserBl)
        {
            this.iuserBl = iuserBl;
        }

        [HttpPost("Register")]
        public ActionResult UserRegisteration(UserRegModel userReg)
        {
            try
            {
                var result = this.iuserBl.UserRegistration( userReg);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<UserRegModel>() { status = true, message = $"Register Successful",Data=result });

                }
                return this.BadRequest(new ResponseModel<UserRegModel>() { status = true, message = $"Register Failed",Data=result});

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost("login")]
        public ActionResult UserLogin(LoginUserModel loginUser)
        {
            try
            {
                var result = this.iuserBl.UserLogin(loginUser);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<LoginUserModel>() { status = true, message = $"Login Successful",Data=result });

                }
                return this.BadRequest(new ResponseModel<LoginUserModel>() { status = true, message = "Login Failed", Data = result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
