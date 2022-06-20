using CommonLayer.Model;
using CommonLayer.WishList;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishListRl : IwishListRl
    {
        private SqlConnection SqlConnection;

        private IConfiguration configuration { get; }

        public WishListRl(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public WishListModel AddWishList(WishListModel wishlistModel, int userId)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("AddWishList", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId ", wishlistModel.BookId);

                    cmd.Parameters.AddWithValue("@UserId ", userId);

                    SqlConnection.Open();

                    int result = cmd.ExecuteNonQuery();

                    SqlConnection.Close();

                    if (result != 0)
                    {
                        return wishlistModel;
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

        public string RemoveWishList(int wishListId, int userId)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("DeleteWishList", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@WishListId ", wishListId);

                    cmd.Parameters.AddWithValue("@UserId ", userId);

                    SqlConnection.Open();

                    int result = cmd.ExecuteNonQuery();

                    SqlConnection.Close();

                    if (result != 0)
                    {
                        return "delete";
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

        public List<ViewWishListModel> GetWishlistDetailsByUserid(int userId)
        {

            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("GetWishListByUserId", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlConnection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<ViewWishListModel> cart = new List<ViewWishListModel>();
                        ViewWishListModel model = new ViewWishListModel();

                        while (reader.Read())
                        {
                            UpdateBookModel bookModel = new UpdateBookModel();
                            bookModel.BookId = Convert.ToInt32(reader["BookId"]);
                            bookModel.BookName = reader["BookName"].ToString();
                            bookModel.AuthorName = reader["AuthorName"].ToString();
                            bookModel.DiscountPrice = reader["DiscountPrice"].ToString();
                            bookModel.ActualPrice = reader["ActualPrice"].ToString();
                            bookModel.BookImage = reader["BookImage"].ToString();
                            model.WishlistId = Convert.ToInt32(reader["WishlistId"]);
                            model.bookId = Convert.ToInt32(reader["BookId"]);
                            model.userId = Convert.ToInt32(reader["userId"]);

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