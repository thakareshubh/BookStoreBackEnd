using CommonLayer.cartModel;
using CommonLayer.Model;
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
                        List<CartResponseModel> cart = new List<CartResponseModel>();
                        CartResponseModel model = new CartResponseModel();

                        while (reader.Read())
                        {
                            UpdateBookModel bookModel = new UpdateBookModel();
                            bookModel. BookId = Convert.ToInt32(reader["BookId"]);
                            bookModel.BookName = reader["BookName"].ToString();
                            bookModel.AuthorName = reader["AuthorName"].ToString();
                            bookModel.DiscountPrice = reader["DiscountPrice"].ToString();
                            bookModel.ActualPrice = reader["ActualPrice"].ToString();
                            bookModel.BookImage = reader["BookImage"].ToString();
                            model.CartId = Convert.ToInt32(reader["BookId"]);
                            model.UserId = Convert.ToInt32(reader["BookId"]);
                            model.BookId = Convert.ToInt32(reader["BookId"]);
                            model.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);
                            model.UpdateBookModel = bookModel;
                            cart.Add(model);
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
