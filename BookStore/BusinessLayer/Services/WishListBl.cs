using BusinessLayer.Interface;
using CommonLayer.WishList;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBl : IwishListBl
    {
        private readonly IwishListRl iwishListRl;

        public WishListBl(IwishListRl iwishListRl)
        {
            this.iwishListRl = iwishListRl;
        }
        public WishListModel AddWishList(WishListModel wishlistModel, int userId)
        {
            try
            {
                return this.iwishListRl.AddWishList(wishlistModel, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ViewWishListModel> GetWishlistDetailsByUserid(int userId)
        {
            try
            {
                return this.iwishListRl.GetWishlistDetailsByUserid( userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string RemoveWishList(int wishListId, int userId)
        {
            try
            {
                return this.iwishListRl.RemoveWishList(wishListId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
