using PhoneShop.Data;
using PhoneShop.Data.Services;
using PhoneShop.Data.Static;
using PhoneShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class SoftwaresController : Controller
    {
        private readonly ISoftwaresService _service;

        public SoftwaresController(ISoftwaresService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Softwares/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Software software)
        {
            if (!ModelState.IsValid)
            {
                return View(software);
            }
            await _service.AddAsync(software);
            return RedirectToAction(nameof(Index));
        }

        //Get: Softwares/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var softwareDetails = await _service.GetByIdAsync(id);

            if (softwareDetails == null) return View("NotFound");
            return View(softwareDetails);
        }

        //Get: Softwares/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var softwareDetails = await _service.GetByIdAsync(id);
            if (softwareDetails == null) return View("NotFound");
            return View(softwareDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Software software)
        {
            if (!ModelState.IsValid)
            {
                return View(software);
            }
            await _service.UpdateAsync(id, software);
            return RedirectToAction(nameof(Index));
        }

        //Get: Softwares/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var softwareDetails = await _service.GetByIdAsync(id);
            if (softwareDetails == null) return View("NotFound");
            return View(softwareDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var softwareDetails = await _service.GetByIdAsync(id);
            if (softwareDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
