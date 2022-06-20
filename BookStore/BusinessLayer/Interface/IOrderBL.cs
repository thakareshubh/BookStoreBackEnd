using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrderBL
    {
        public OrderModel AddOrder(OrderModel orderModel, int userId);
        public List<ViewOrderModel> GetAllOrder(int userId);
    }
}
