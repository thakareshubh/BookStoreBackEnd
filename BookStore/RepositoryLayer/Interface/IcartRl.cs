using CommonLayer.cartModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IcartRl
    {
        cartModel AddtoCart(cartModel cart, int userId);

        public string removeCart(int cartId);
        public cartModel updateCart(int cartId, cartModel cartModel, int userId);

        List<CartResponseModel> GetAllCart(int userId);
    }
}
