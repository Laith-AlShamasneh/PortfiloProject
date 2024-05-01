using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfilo.Models;
using Portfilo.Models.Repository;
using System.Security.Claims;

namespace Portfilo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MenuController : Controller
    {
        private readonly IRepository<Menu> menu;

        public MenuController(IRepository<Menu> menu)
        {
            this.menu = menu;
        }

        public async Task<IActionResult> Index(int idDelete)
        {
            if (idDelete != 0)
            {
                var obj = await menu.FindAsync(idDelete);
                obj.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                obj.EditDate = DateTime.UtcNow;
                await menu.DeleteAsync(idDelete, obj);
                return RedirectToAction(nameof(Index));
            }
            var data = await menu.GetAllForAdminAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Menu collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Please fill all required fields!");
                    return View(collection);
                }
                collection.CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                collection.CreateDate = DateTime.UtcNow;
                collection.IsActive = true;
                await menu.AddAsync(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(collection);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await menu.FindAsync(id);
            if (data is null)
            {
                return View();
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Menu collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Please fill all required fields!");
                    return View(collection);
                }
                collection.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                collection.EditDate = DateTime.UtcNow;
                await menu.UpdateAsync(id, collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Active(int id)
        {
            try
            {
                await menu.ActiveAsync(id, new Menu()
                {
                    EditDate = DateTime.UtcNow,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                });
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
        }
    }
}