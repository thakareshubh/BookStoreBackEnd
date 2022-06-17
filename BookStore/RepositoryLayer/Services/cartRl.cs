using CommonLayer.cartModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class cartRl : IcartRl
    {
        private SqlConnection SqlConnection;

        private IConfiguration configuration { get; }

        public cartRl(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public cartModel AddtoCart(cartModel cart, int userId)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spAddCart", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId ", cart.BookId);
                    cmd.Parameters.AddWithValue("@BookQuantity ", cart.BookQuantity);
                    cmd.Parameters.AddWithValue("@UserId ", userId);
                  

                    SqlConnection.Open();

                    int result = cmd.ExecuteNonQuery();

                    SqlConnection.Close();

                    if (result != 0)
                    {
                        return cart;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string removeCart(int cartId)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spRemoveCart", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CartId ", cartId);


                    SqlConnection.Open();

                    int result = cmd.ExecuteNonQuery();

                    SqlConnection.Close();

                    if (result == 0)
                    {
                        return "Failed";
                    }
                    else
                    {
                        return "deleted";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CartResponseModel> GetAllCart(int userId)
        {
            List<CartResponseModel> cart = new List<CartResponseModel>();
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spGetAllCart", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlConnection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            cart.Add(new CartResponseModel
                            {
                                BookId = Convert.ToInt32(reader["BookId"]),
                                BookName = reader["BookName"].ToString(),
                                AuthorName = reader["AuthorName"].ToString(),
                                CartId = Convert.ToInt32(reader["CartId"]),
                                DiscountPrice = reader["DiscountPrice"].ToString(),
                                ActualPrice = reader["ActualPrice"].ToString(),
                                BookImage = reader["BookImage"].ToString(),
                                BookQuantity = Convert.ToInt32(reader["BookQuantity"]),
                            });

                        }
                        SqlConnection.Close();
                        return cart;
                    }


                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
