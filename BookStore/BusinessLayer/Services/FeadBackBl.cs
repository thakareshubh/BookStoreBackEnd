using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeadBackBl : IfeadbackBl
    {
        private readonly IfeadBackRl ifeadbackRl;

        public FeadBackBl(IfeadBackRl ifeadbackRl)
        {
            this.ifeadbackRl = ifeadbackRl;
        }
        public FeedBackModel AddFeedback(FeedBackModel feedback, int userId)
        {
            try
            {
                return this.ifeadbackRl.AddFeedback(feedback, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<feadbackResponseModel> GetRecordsByBookId(int bookId)
        {
            try
            {
                return this.ifeadbackRl.GetRecordsByBookId(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
