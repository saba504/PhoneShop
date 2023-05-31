using PhoneShop.Data.Base;
using PhoneShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Data.Services
{
    public class SoftwaresService : EntityBaseRepository<Software>, ISoftwaresService
    {
        public SoftwaresService(AppDbContext context) : base(context) { }
    }
}
