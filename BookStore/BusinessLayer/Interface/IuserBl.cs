using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IuserBl
    {
        public UserRegModel UserRegistration(UserRegModel userReg);

        public LoginUserModel UserLogin(string Email, string Password);

        public string UserForgotPassword(string Email);

        public string UserResetPassword(resetPasswordModel resetPassword, string email);
    }
}
