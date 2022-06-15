using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRl : IuserRl
    {
        private SqlConnection SqlConnection;

        
        private IConfiguration configuration { get; }
        /// the configuration is stored in name-value pairs and it can be read at runtime from various parts of the application

        public UserRl(IConfiguration configuration)
        {
            this.configuration = configuration;
            
        }
        

        public UserRegModel UserRegistration(UserRegModel userReg)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SP_User_Registration", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //var encryptedPassword = EncryptPassword(userReg.Password);

                    cmd.Parameters.AddWithValue("@FullName", userReg.FullName);
                    cmd.Parameters.AddWithValue("@Email", userReg.Email);
                    cmd.Parameters.AddWithValue("@Password", userReg.Password);
                    cmd.Parameters.AddWithValue("@MobileNumber", userReg.MobileNumber);
                    SqlConnection.Open();


                    int result = cmd.ExecuteNonQuery();
                    ///ExecuteNonQuery method is used to execute SQL Command or the storeprocedure performs, INSERT, UPDATE or Delete operations.
                    ///It doesn't return any data from the database.
                    ///Instead, it returns an integer specifying the number of rows inserted, updated or deleted.


                    SqlConnection.Close();
                    if (result != 0)
                    {
                        return userReg;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

      

       

        public LoginUserModel UserLogin(LoginUserModel userLogin)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);
            try
            {
               if(userLogin.Email == null || userLogin.Password ==null)
                {
                    return null;
                }
               else
                {
                    using (SqlConnection)
                    {
                        SqlCommand cmd = new SqlCommand("spLogin", SqlConnection);
                        { cmd.CommandType = CommandType.StoredProcedure; }

                       

                        cmd.Parameters.AddWithValue("@Email", userLogin.Email);
                        cmd.Parameters.AddWithValue("@Password", userLogin.Password);

                        SqlConnection.Open();

                        SqlDataReader sdr = cmd.ExecuteReader();
                        ///ExecuteReader method is used to execute a SQL Command or storedprocedure returns a set of rows from the database.
                        ///


                        if (sdr.HasRows)
                        {
                            ///The HasRows property returns information about the current result set.


                            LoginUserModel model = new LoginUserModel();
                            while (sdr.Read())
                            {
                               

                                model.Email = Convert.ToString(sdr["Email"]);
                                model.Password = Convert.ToString(sdr["Password"]);
                                var UserId = Convert.ToInt32(sdr["UserId"]);

                            }
                            this.SqlConnection.Close();
                           
                            return model;

                        }

                        else
                        {
                            this.SqlConnection.Close();
                            return null ;
                        }

                        
                    }
                   
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
           
           
            
        }

      

    }
}
