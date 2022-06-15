using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IuserBl
    {
        public UserRegModel UserRegistration(UserRegModel userReg);

        public LoginUserModel UserLogin(LoginUserModel userLogin);
    }
}
