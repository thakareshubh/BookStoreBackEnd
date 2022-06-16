using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAdminBl
    {
        public AddminLoginModel AdminLogin(string Email, string Password);
    }
}
