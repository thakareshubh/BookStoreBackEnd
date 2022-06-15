using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBl : IuserBl
    {
        private readonly IuserRl iuserRl;

        public UserBl(IuserRl iuserRl)
        {
            this.iuserRl = iuserRl;
        }

      

        //register
        public UserRegModel UserRegistration(UserRegModel userReg)
        {
            try
            {
                return this.iuserRl.UserRegistration( userReg);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //login
        public LoginUserModel UserLogin(string Email, string Password)
        {
            try
            {
                return this.iuserRl.UserLogin(Email, Password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UserForgotPassword(string Email)
        {
            try
            {
                return this.iuserRl.UserForgotPassword(Email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UserResetPassword(resetPasswordModel resetPassword, string email)
        {
            try
            {
                return this.iuserRl.UserResetPassword(resetPassword, email);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
