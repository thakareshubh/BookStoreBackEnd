using CommonLayer.Model;
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
        public UpdateBookModel UpdateBookModel { get; set; }

    }
}
