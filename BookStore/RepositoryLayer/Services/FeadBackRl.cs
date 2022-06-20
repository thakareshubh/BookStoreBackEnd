using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FeadBackRl : IfeadBackRl
    {
        private SqlConnection SqlConnection;

        private IConfiguration configuration { get; }

        public FeadBackRl(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public FeedBackModel AddFeedback(FeedBackModel feedback, int userId)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("AddFeedback", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId ", feedback.BookId);
                    cmd.Parameters.AddWithValue("@Rating ", feedback.Rating);
                    cmd.Parameters.AddWithValue("@Comment ", feedback.Comment);
                    cmd.Parameters.AddWithValue("@UserId ", userId);


                    SqlConnection.Open();

                    int result = cmd.ExecuteNonQuery();

                    SqlConnection.Close();

                    if (result != 0)
                    {
                        return feedback;
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

        public List<feadbackResponseModel> GetRecordsByBookId(int bookId)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("GetAllFeedback", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    SqlConnection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<feadbackResponseModel> cart = new List<feadbackResponseModel>();
                        feadbackResponseModel model = new feadbackResponseModel();
                       
                        while (reader.Read())
                        {
                            model.FeedbackId = Convert.ToInt32(reader["FeedbackId"]);
                            model.FullName = reader["FullName"].ToString();
                            model.Comment = reader["Comment"].ToString();
                            model.Rating = Convert.ToInt32(reader["Rating"]);
                            model.BookId = Convert.ToInt32(reader["BookId"]);
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
