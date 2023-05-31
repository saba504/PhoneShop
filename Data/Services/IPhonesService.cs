using PhoneShop.Data.Base;
using PhoneShop.Data.ViewModels;
using PhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Data.Services
{
    public interface IPhonesService:IEntityBaseRepository<Phone>
    {
        Task<Phone> GetPhoneByIdAsync(int id);
        Task<NewPhoneDropdownsVM> GetNewPhoneDropdownsValues();
        Task AddNewPhoneAsync(NewPhoneVM data);
        Task UpdatePhoneAsync(NewPhoneVM data);
    }
}
