using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBl : IAdminBl
    {
        private readonly IAdminRl iadminRl;

        public AdminBl(IAdminRl iadminRl)
        {
            this.iadminRl = iadminRl;
        }

        public AddminLoginModel AdminLogin(string Email, string Password)
        {
            try
            {
                return this.iadminRl.AdminLogin(Email, Password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
