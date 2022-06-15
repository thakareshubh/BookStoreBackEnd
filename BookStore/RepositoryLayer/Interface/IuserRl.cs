using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IuserRl
    {
        public UserRegModel UserRegistration( UserRegModel userReg);

        public LoginUserModel UserLogin( LoginUserModel userLogin);
    }
}
