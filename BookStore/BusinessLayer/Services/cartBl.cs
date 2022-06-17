using BusinessLayer.Interface;
using CommonLayer.cartModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class cartBl : IcartBl
    {
        private readonly IcartRl icartRl;

        public cartBl(IcartRl icartRl)
        {
            this.icartRl = icartRl;
        }

        public cartModel AddtoCart(cartModel cart, int userId)
        {

            try
            {
                return this.icartRl.AddtoCart(cart, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CartResponseModel> GetAllCart(int userId)
        {
            try
            {
                return this.icartRl.GetAllCart(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string removeCart(int cartId)
        {
            try
            {
                return this.icartRl.removeCart(cartId);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
