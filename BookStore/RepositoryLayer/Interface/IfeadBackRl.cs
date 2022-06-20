using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IfeadBackRl
    {
        public FeedBackModel AddFeedback(FeedBackModel feedback, int userId);
        public List<feadbackResponseModel> GetRecordsByBookId(int bookId);
    }
}
