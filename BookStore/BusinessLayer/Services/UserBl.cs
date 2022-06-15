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
        public LoginUserModel UserLogin(LoginUserModel userLogin)
        {
            try
            {
                return this.iuserRl.UserLogin(userLogin);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
