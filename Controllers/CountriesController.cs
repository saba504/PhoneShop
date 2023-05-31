using PhoneShop.Data;
using PhoneShop.Data.Services;
using PhoneShop.Data.Static;
using PhoneShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CountriesController : Controller
    {
        private readonly ICountriesService _service;

        public CountriesController(ICountriesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCountries = await _service.GetAllAsync();
            return View(allCountries);
        }

        //GET:
        ///details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var countryDetails = await _service.GetByIdAsync(id);
            if (countryDetails == null) return View("NotFound");
            return View(countryDetails);
        }

        //GET: countrys/create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")]Country country)
        {
            if (!ModelState.IsValid) return View(country);

            await _service.AddAsync(country);
            return RedirectToAction(nameof(Index));
        }

        //GET: countrys/edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var countryDetails = await _service.GetByIdAsync(id);
            if (countryDetails == null) return View("NotFound");
            return View(countryDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Country country)
        {
            if (!ModelState.IsValid) return View(country);

            if(id == country.Id)
            {
                await _service.UpdateAsync(id, country);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        //GET: countrys/delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var countryDetails = await _service.GetByIdAsync(id);
            if (countryDetails == null) return View("NotFound");
            return View(countryDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var countryDetails = await _service.GetByIdAsync(id);
            if (countryDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
