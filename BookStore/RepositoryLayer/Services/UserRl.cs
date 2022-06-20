using CommonLayer.Model;
using Experimental.System.Messaging;
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
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);//DataBase connection

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SP_User_Registration", SqlConnection);// connection with store procedure
                    cmd.CommandType = CommandType.StoredProcedure;
                    var encryptedPassword = EncryptPassword(userReg.Password);

                    cmd.Parameters.AddWithValue("@FullName", userReg.FullName);
                    cmd.Parameters.AddWithValue("@Email", userReg.Email);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);
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

        public static string EncryptPassword(string password)
        {
            try
            {
                if (password == null)
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DecryptedPassword(string encryptedPassword)
        {
            byte[] b;
            string decrypted;
            try
            {
                if (encryptedPassword == null)
                {
                    return null;
                }
                else
                {
                    b = Convert.FromBase64String(encryptedPassword);
                    decrypted = Encoding.ASCII.GetString(b);
                    return decrypted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public LoginUserModel UserLogin(string Email, string Password)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);
            try
            {
                if (Email == null || Password == null)
                {
                    return null;
                }
                else
                {
                    using (SqlConnection)
                    {
                        SqlCommand cmd = new SqlCommand("spLogin", SqlConnection);
                        { cmd.CommandType = CommandType.StoredProcedure; }

                        LoginUserModel model = new LoginUserModel();
                        var encryptedPassword = EncryptPassword(Password);

                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Password", encryptedPassword);

                        SqlConnection.Open();

                        SqlDataReader sdr = cmd.ExecuteReader();
                        ///ExecuteReader method is used to execute a SQL Command or storedprocedure returns a set of rows from the database.
                        ///


                        if (sdr.HasRows)
                        {
                            ///The HasRows property returns information about the current result set.

                            int UserId = 0;

                            while (sdr.Read())
                            {


                                model.Email = Convert.ToString(sdr["Email"]);
                                encryptedPassword = Convert.ToString(sdr["Password"]);
                                UserId = Convert.ToInt32(sdr["UserId"]);



                            }
                            this.SqlConnection.Close();
                            model.Token = this.GenerateSecurityToken(model.Email, UserId);

                            return model;

                        }

                        else
                        {
                            this.SqlConnection.Close();
                            return null;
                        }


                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Token for login
        public string GenerateSecurityToken(string emailID, int userId)
        {
            var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN"));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"User"),
                new Claim(ClaimTypes.Email, emailID),
                new Claim("UserId", userId.ToString())
            };
            var token = new JwtSecurityToken(
                this.configuration["Jwt:Issuer"],
                this.configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        private void MsmqQueue_ReciveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                {
                    MessageQueue queue = (MessageQueue)sender;
                    Message msg = queue.EndReceive(e.AsyncResult);
                    EmailServices.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                    queue.BeginReceive();
                }

            }
            catch (MessageQueueException ex)
            {

                if (ex.MessageQueueErrorCode ==
                   MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }

            }


        }

        private string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", email)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string UserForgotPassword(string Email)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);
            try
            {

                SqlCommand com = new SqlCommand("spUserForgotPassword", this.SqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Email", Email);
                this.SqlConnection.Open();
                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.HasRows)
                {
                    int UserId = 0;
                    while (rdr.Read())
                    {
                        Email = Convert.ToString(rdr["Email"]);
                        UserId = Convert.ToInt32(rdr["UserId"]);
                    }

                    this.SqlConnection.Close();
                    MessageQueue BookstoreQ;

                    if (MessageQueue.Exists(@".\Private$\BookstoreQueue"))
                        BookstoreQ = new MessageQueue(@".\Private$\BookstoreQueue");
                    else BookstoreQ = MessageQueue.Create(@".\Private$\BookstoreQueue");

                    Message message = new Message();
                    message.Formatter = new BinaryMessageFormatter();
                    message.Body = GenerateSecurityToken(Email, UserId);
                    EmailServices.SendMail(Email, message.Body.ToString());
                    BookstoreQ.ReceiveCompleted += new ReceiveCompletedEventHandler(MsmqQueue_ReciveCompleted);

                    var token = this.GenerateSecurityToken(Email, UserId);

                    return token;
                }
                else
                {
                    this.SqlConnection.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string UserResetPassword(resetPasswordModel resetPassword, string email)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);
            try
            {
                if (resetPassword.NewPassword == resetPassword.ConfirmPassword)
                {
                    SqlCommand cmd = new SqlCommand("spUserResetPassword", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //var encryptPassword = EncryptPassword(resetPassword.NewPassword);

                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", EncryptPassword(resetPassword.NewPassword));

                    SqlConnection.Open();

                    var result = cmd.ExecuteNonQuery();
                    SqlConnection.Close();

                    if (result != 0)
                    {
                        return "Congratulations! Your password has been changed successfully";
                    }
                    else
                        return "Failed to reset your password";
                }
                else
                {
                    return "Make sure password are matched";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
