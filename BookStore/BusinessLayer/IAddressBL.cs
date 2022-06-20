using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IAddressBL
    {
        public string AddAddress(AddAddressModel addAddress, int userId);
        public string DeleteAddress(int AddressId, int UserId);
        public AddressModel UpdateAddress(AddressModel addressModel, int userId);
    }
}
