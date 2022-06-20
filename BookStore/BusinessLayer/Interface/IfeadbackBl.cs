using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IfeadbackBl
    {
        public FeedBackModel AddFeedback(FeedBackModel feedback, int userId);
        public List<feadbackResponseModel> GetRecordsByBookId(int bookId);
    }
}
