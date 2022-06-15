using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

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
        public ActionResult UserLogin(string Email, string Password)
        {
            try
            {
                var result = this.iuserBl.UserLogin(Email, Password);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<LoginUserModel>() { status = true, message = $"Login Successful",Data=result });

                }
                return this.BadRequest(new ResponseModel<LoginUserModel>() { status = true, message = "Login Failed",Data=result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("ForgotPassword")]
        public IActionResult UserForgotPassword(string Email)
        {
            try
            {
                var forgotPasswordToken = this.iuserBl.UserForgotPassword(Email);
                if (forgotPasswordToken != null)
                {
                    return this.Ok(new { Success = true, message = " Mail Sent Successful", Response = forgotPasswordToken });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Email" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        
        [HttpPut("ResetPassword")]

        public ActionResult UserResetPassword(resetPasswordModel resetPassword)
        {
            try
            {

                string email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                string result = iuserBl.UserResetPassword(resetPassword, email);
                if (result==null)
                {
                    return this.BadRequest(new ResponseModel<string>() { status = false, message = $"Reset Password Failed", Data = result });

                }

                return this.Ok(new ResponseModel<string>() { status = true, message = $"Reset password Succesful", Data = result });
            }
            catch
            {
                throw;
            }
        }


    }
}
