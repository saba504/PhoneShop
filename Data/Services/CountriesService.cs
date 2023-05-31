using PhoneShop.Data.Base;
using PhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Data.Services
{
    public class CountriesService: EntityBaseRepository<Country>, ICountriesService
    {
        public CountriesService(AppDbContext context) : base(context)
        {
        }
    }
}
