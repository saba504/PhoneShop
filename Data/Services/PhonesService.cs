using PhoneShop.Data.Base;
using PhoneShop.Data.ViewModels;
using PhoneShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Data.Services
{
    public class PhonesService : EntityBaseRepository<Phone>, IPhonesService
    {
        private readonly AppDbContext _context;
        public PhonesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewPhoneAsync(NewPhoneVM data)
        {
            var newPhone = new Phone()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                BrandId = data.BrandId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                PhoneCategory = data.PhoneCategory,
                CountryId = data.CountryId
            };
            await _context.Phones.AddAsync(newPhone);
            await _context.SaveChangesAsync();

            //Add Phone Softwares
            foreach (var SoftwareId in data.SoftwareIds)
            {
                var newSoftwarePhone = new Software_Phone()
                {
                    PhoneId = newPhone.Id,
                    SoftwareId = SoftwareId
                };
                await _context.Softwares_Phones.AddAsync(newSoftwarePhone);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Phone> GetPhoneByIdAsync(int id)
        {
            var phoneDetails = await _context.Phones
                .Include(c => c.Brand)
                .Include(p => p.Country)
                .Include(am => am.Softwares_Phones).ThenInclude(a => a.Software)
                .FirstOrDefaultAsync(n => n.Id == id);

            return phoneDetails;
        }

        public async Task<NewPhoneDropdownsVM> GetNewPhoneDropdownsValues()
        {
            var response = new NewPhoneDropdownsVM()
            {
                Softwares = await _context.Softwares.OrderBy(n => n.FullName).ToListAsync(),
                Brands = await _context.Brands.OrderBy(n => n.Name).ToListAsync(),
                Countries = await _context.Countries.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdatePhoneAsync(NewPhoneVM data)
        {
            var dbPhone = await _context.Phones.FirstOrDefaultAsync(n => n.Id == data.Id);

            if(dbPhone != null)
            {
                dbPhone.Name = data.Name;
                dbPhone.Description = data.Description;
                dbPhone.Price = data.Price;
                dbPhone.ImageURL = data.ImageURL;
                dbPhone.BrandId = data.BrandId;
                dbPhone.StartDate = data.StartDate;
                dbPhone.EndDate = data.EndDate;
                dbPhone.PhoneCategory = data.PhoneCategory;
                dbPhone.CountryId = data.CountryId;
                await _context.SaveChangesAsync();
            }

            //Remove existing Softwares
            var existingSoftwaresDb = _context.Softwares_Phones.Where(n => n.PhoneId == data.Id).ToList();
            _context.Softwares_Phones.RemoveRange(existingSoftwaresDb);
            await _context.SaveChangesAsync();

            //Add Phone Softwares
            foreach (var SoftwareId in data.SoftwareIds)
            {
                var newSoftwarePhone = new Software_Phone()
                {
                    PhoneId = data.Id,
                    SoftwareId = SoftwareId
                };
                await _context.Softwares_Phones.AddAsync(newSoftwarePhone);
            }
            await _context.SaveChangesAsync();
        }
    }
}
