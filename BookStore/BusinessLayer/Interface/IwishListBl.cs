﻿using CommonLayer.WishList;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IwishListBl
    {
        public WishListModel AddWishList(WishListModel wishlistModel, int userId);
        public string RemoveWishList(int wishListId, int userId);
        public List<ViewWishListModel> GetWishlistDetailsByUserid(int userId);
    }
}
