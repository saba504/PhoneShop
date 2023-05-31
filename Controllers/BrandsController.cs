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
    public class BrandsController : Controller
    {
        private readonly IBrandsService _service;

        public BrandsController(IBrandsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allBrands = await _service.GetAllAsync();
            return View(allBrands);
        }


        //Get: Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")]Brand brand)
        {
            if (!ModelState.IsValid) return View(brand);
            await _service.AddAsync(brand);
            return RedirectToAction(nameof(Index));
        }

        //Get: Brands/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var BrandDetails = await _service.GetByIdAsync(id);
            if (BrandDetails == null) return View("NotFound");
            return View(BrandDetails);
        }

        //Get: Brands/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var BrandDetails = await _service.GetByIdAsync(id);
            if (BrandDetails == null) return View("NotFound");
            return View(BrandDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Brand brand)
        {
            if (!ModelState.IsValid) return View(brand);
            await _service.UpdateAsync(id, brand);
            return RedirectToAction(nameof(Index));
        }

        //Get: Brands/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var BrandDetails = await _service.GetByIdAsync(id);
            if (BrandDetails == null) return View("NotFound");
            return View(BrandDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var BrandDetails = await _service.GetByIdAsync(id);
            if (BrandDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
