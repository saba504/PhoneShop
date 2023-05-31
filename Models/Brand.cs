using PhoneShop.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Models
{
    public class Brand:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Brand Logo")]
        [Required(ErrorMessage = "Brand logo is required")]
        public string Logo { get; set; }

        [Display(Name = "Brand Name")]
        [Required(ErrorMessage = "Brand name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Brand description is required")]
        public string Description { get; set; }

        //Relationships
        public List<Phone> Phones { get; set; }
    }
}
