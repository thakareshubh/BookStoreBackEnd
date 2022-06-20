using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.WishList
{
    public class ViewWishListModel
    {
        public int WishlistId { get; set; }
        public int userId { get; set; }
        public int bookId { get; set; }
        public UpdateBookModel UpdateBookModel { get; set; }
    }
}
