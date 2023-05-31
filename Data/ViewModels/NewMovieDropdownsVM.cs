using PhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Data.ViewModels
{
    public class NewPhoneDropdownsVM
    {
        public NewPhoneDropdownsVM()
        {
            Countries = new List<Country>();
            Brands = new List<Brand>();
            Softwares = new List<Software>();
        }

        public List<Country> Countries { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Software> Softwares { get; set; }
    }
}
