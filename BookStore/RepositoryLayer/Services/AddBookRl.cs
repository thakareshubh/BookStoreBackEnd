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
    public class AddBookRl : IAddbookRl
    {
        private SqlConnection SqlConnection;

        private IConfiguration configuration { get; }

        public AddBookRl(IConfiguration configuration)
        {
            this.configuration = configuration;

        }
        public AddBookModel AddBook(AddBookModel addBookModel)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("Addbook", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookName ", addBookModel.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", addBookModel.AuthorName);
                    cmd.Parameters.AddWithValue("@Rating ", addBookModel.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", addBookModel.RatingCount);
                    cmd.Parameters.AddWithValue("@DiscountPrice ", addBookModel.DiscountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", addBookModel.ActualPrice);
                    cmd.Parameters.AddWithValue("@Description ", addBookModel.Description);
                    cmd.Parameters.AddWithValue("@BookImage", addBookModel.BookImage);
                    cmd.Parameters.AddWithValue("@BookQuantity", addBookModel.BookQuantity);

                    SqlConnection.Open();

                    int result = cmd.ExecuteNonQuery();

                    SqlConnection.Close();

                    if (result != 0)
                    {
                        return addBookModel;
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

        public UpdateBookModel UpdateBook(UpdateBookModel UpdateBookModel)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("Updatebook", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId ", UpdateBookModel.BookId);
                    cmd.Parameters.AddWithValue("@BookName ", UpdateBookModel.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", UpdateBookModel.AuthorName);
                    cmd.Parameters.AddWithValue("@Rating ", UpdateBookModel.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", UpdateBookModel.RatingCount);
                    cmd.Parameters.AddWithValue("@DiscountPrice ", UpdateBookModel.DiscountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", UpdateBookModel.ActualPrice);
                    cmd.Parameters.AddWithValue("@Description ", UpdateBookModel.Description);
                    cmd.Parameters.AddWithValue("@BookImage", UpdateBookModel.BookImage);
                    cmd.Parameters.AddWithValue("@BookQuantity", UpdateBookModel.BookQuantity);

                    SqlConnection.Open();

                    int result = cmd.ExecuteNonQuery();

                    SqlConnection.Close();

                    if (result != 0)
                    {
                        return UpdateBookModel;
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

        public string DeleteBook(int BookId)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("Deletebook", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId ", BookId);
                   

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

        public AddBookModel GetBook(int BookId)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("GetBookById", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId ", BookId);


                    SqlConnection.Open();

                    
                    

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        AddBookModel model = new AddBookModel();
                        while (reader.Read())
                        {
                            BookId = Convert.ToInt32(reader["BookId"]);
                            model.BookName=reader["BookName"].ToString();
                            model.AuthorName = reader["AuthorName"].ToString();
                            model.Rating = Convert.ToInt32(reader["Rating"]);
                            model.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                            model.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                            model.ActualPrice = Convert.ToInt32(reader["ActualPrice"]);
                            model.BookImage = reader["BookImage"].ToString();
                            model.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);

                        }
                        SqlConnection.Close();
                        return model;
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

        public List<GetAllBookModel> GetAllBooks()
        {
            List<GetAllBookModel> books = new List<GetAllBookModel>();
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("GetAllBooks", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlConnection.Open();




                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        
                        while (reader.Read())
                        {
                            books.Add(new GetAllBookModel
                            {
                                BookId = Convert.ToInt32(reader["BookId"]),
                                BookName = reader["BookName"].ToString(),
                                AuthorName = reader["AuthorName"].ToString(),
                                Rating = Convert.ToInt32(reader["Rating"]),
                                RatingCount = Convert.ToInt32(reader["RatingCount"]),
                                DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]),
                                ActualPrice = Convert.ToInt32(reader["ActualPrice"]),
                                BookImage = reader["BookImage"].ToString(),
                                BookQuantity = Convert.ToInt32(reader["BookQuantity"]),
                            });

                        }
                        SqlConnection.Close();
                        return books;
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
