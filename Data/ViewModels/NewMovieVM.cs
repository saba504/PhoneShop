using PhoneShop.Data;
using PhoneShop.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Models
{
    public class NewPhoneVM
    {
        public int Id { get; set; }

        [Display(Name = "Phone name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Phone description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Phone poster URL")]
        [Required(ErrorMessage = "Phone poster URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Phone start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Phone end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Phone category is required")]
        public PhoneCategory PhoneCategory { get; set; }

        //Relationships
        [Display(Name = "Select Software(s)")]
        [Required(ErrorMessage = "Phone Software(s) is required")]
        public List<int> SoftwareIds { get; set; }

        [Display(Name = "Select a Brand")]
        [Required(ErrorMessage = "Phone Brand is required")]
        public int BrandId { get; set; }

        [Display(Name = "Select a country")]
        [Required(ErrorMessage = "Phone country is required")]
        public int CountryId { get; set; }
    }
}
