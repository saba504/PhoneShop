using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Models
{
    public class Software_Phone
    {
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }

        public int SoftwareId { get; set; }
        public Software Software { get; set; }
    }
}
