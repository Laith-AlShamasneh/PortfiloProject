using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfilo.Models.Repository;
using Portfilo.Models;
using System.Security.Claims;
using Portfilo.ViewModels;

namespace Portfilo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HeadersController : Controller
    {
        private readonly IRepository<Header> menu;
        private readonly IWebHostEnvironment hosting;

        public HeadersController(IRepository<Header> menu, IWebHostEnvironment hosting)
        {
            this.menu = menu;
            this.hosting = hosting;
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
        public async Task<IActionResult> Create(HeaderModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Please fill all required fields!");
                    return View(collection);
                }

                string imagefile = UploadImage(collection.ImageFile);

                var newData = new Header
                {
                    HeaderId = collection.HeaderId,
                    Title = collection.Title,
                    Brief = collection.Brief,
                    Description = collection.Description,
                    Url1 = collection.Url1,
                    Url2 = collection.Url2,
                    Image = imagefile,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    CreateDate = DateTime.UtcNow,
                    IsActive = true
                };

                await menu.AddAsync(newData);
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
            var data2 = new HeaderModel
            {
                HeaderId = data.HeaderId,
                Title = data.Title,
                Brief = data.Brief,
                Description = data.Description,
                Url1 = data.Url1,
                Url2 = data.Url2,
                Image = data.Image,
            };
            if (data is null)
            {
                return View();
            }

            return View(data2);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HeaderModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Please fill all required fields!");
                    return View(collection);
                }

                var data = await menu.FindAsync(id);
                string imagefile = UploadImage(collection.ImageFile);

                var newData = new Header
                {
                    HeaderId = collection.HeaderId,
                    Title = collection.Title,
                    Brief = collection.Brief,
                    Description = collection.Description,
                    Url1 = collection.Url1,
                    Url2 = collection.Url2,
                    Image = (imagefile != "") ? imagefile : collection.Image,
                    CreateUser = data.CreateUser,
                    CreateDate = data.CreateDate,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow
                };

                await menu.UpdateAsync(id, newData);
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
                await menu.ActiveAsync(id, new Header()
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

        public string UploadImage(IFormFile file)
        {
            string imageName = "";
            if (file is not null)
            {
                string Folder = Path.Combine("Images", "Header");
                string pathFile = Path.Combine(hosting.WebRootPath,Folder);
                Directory.CreateDirectory(pathFile);
                FileInfo fileInfo = new(file.FileName);
                imageName = "Image_" + Guid.NewGuid() + fileInfo.Extension;
                string fullPath = Path.Combine(pathFile, imageName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));
            }
            return imageName;
        }
    }
}
