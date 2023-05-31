using PhoneShop.Data;
using PhoneShop.Data.Services;
using PhoneShop.Data.Static;
using PhoneShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class PhonesController : Controller
    {
        private readonly IPhonesService _service;

        public PhonesController(IPhonesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allPhones = await _service.GetAllAsync(n => n.Brand);
            return View(allPhones);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allPhones = await _service.GetAllAsync(n => n.Brand);

            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allPhones.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allPhones.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allPhones);
        }

        //GET: Phones/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var phoneDetail = await _service.GetPhoneByIdAsync(id);
            return View(phoneDetail);
        }

        //GET: Phones/Create
        public async Task<IActionResult> Create()
        {
            var phoneDropdownsData = await _service.GetNewPhoneDropdownsValues();

            ViewBag.Brands = new SelectList(phoneDropdownsData.Brands, "Id", "Name");
            ViewBag.Countries = new SelectList(phoneDropdownsData.Countries, "Id", "FullName");
            ViewBag.Softwares = new SelectList(phoneDropdownsData.Softwares, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewPhoneVM phone)
        {
            if (!ModelState.IsValid)
            {
                var phoneDropdownsData = await _service.GetNewPhoneDropdownsValues();

                ViewBag.Brands = new SelectList(phoneDropdownsData.Brands, "Id", "Name");
                ViewBag.Countries = new SelectList(phoneDropdownsData.Countries, "Id", "FullName");
                ViewBag.Softwares = new SelectList(phoneDropdownsData.Softwares, "Id", "FullName");

                return View(phone);
            }

            await _service.AddNewPhoneAsync(phone);
            return RedirectToAction(nameof(Index));
        }


        //GET: Phones/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var phoneDetails = await _service.GetPhoneByIdAsync(id);
            if (phoneDetails == null) return View("NotFound");

            var response = new NewPhoneVM()
            {
                Id = phoneDetails.Id,
                Name = phoneDetails.Name,
                Description = phoneDetails.Description,
                Price = phoneDetails.Price,
                StartDate = phoneDetails.StartDate,
                EndDate = phoneDetails.EndDate,
                ImageURL = phoneDetails.ImageURL,
                PhoneCategory = phoneDetails.PhoneCategory,
                BrandId = phoneDetails.BrandId,
                CountryId = phoneDetails.CountryId,
                SoftwareIds = phoneDetails.Softwares_Phones.Select(n => n.SoftwareId).ToList(),
            };

            var phoneDropdownsData = await _service.GetNewPhoneDropdownsValues();
            ViewBag.Brands = new SelectList(phoneDropdownsData.Brands, "Id", "Name");
            ViewBag.Countries = new SelectList(phoneDropdownsData.Countries, "Id", "FullName");
            ViewBag.Softwares = new SelectList(phoneDropdownsData.Softwares, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewPhoneVM phone)
        {
            if (id != phone.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var phoneDropdownsData = await _service.GetNewPhoneDropdownsValues();

                ViewBag.Brands = new SelectList(phoneDropdownsData.Brands, "Id", "Name");
                ViewBag.Countries = new SelectList(phoneDropdownsData.Countries, "Id", "FullName");
                ViewBag.Softwares = new SelectList(phoneDropdownsData.Softwares, "Id", "FullName");

                return View(phone);
            }

            await _service.UpdatePhoneAsync(phone);
            return RedirectToAction(nameof(Index));
        }
    }
}