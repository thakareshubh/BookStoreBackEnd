using CommonLayer.cartModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IcartBl
    {
        cartModel AddtoCart(cartModel cart, int userId);
        public string removeCart(int cartId);
        List<CartResponseModel> GetAllCart(int userId);
    }
}
