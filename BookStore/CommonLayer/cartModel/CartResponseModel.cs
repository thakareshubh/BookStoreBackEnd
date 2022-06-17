using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.cartModel
{
    public class CartResponseModel
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int BookQuantity { get; set; }
        public string BookName { get; set; }
        public string BookImage { get; set; }
        public string AuthorName { get; set; }
        public string DiscountPrice { get; set; }
        public string ActualPrice { get; set; }

    }
}
