using PhoneShop.Data.Base;
using PhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Data.Services
{
    public class BrandsService:EntityBaseRepository<Brand>, IBrandsService
    {
        public BrandsService(AppDbContext context) : base(context)
        {
        }
    }
}
