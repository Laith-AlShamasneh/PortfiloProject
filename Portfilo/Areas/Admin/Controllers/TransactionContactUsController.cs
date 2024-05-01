using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfilo.Models.Repository;
using Portfilo.Models;
using System.Security.Claims;

namespace Portfilo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TransactionContactUsController : Controller
    {
        private readonly IRepository<TransactionContactUs> menu;

        public TransactionContactUsController(IRepository<TransactionContactUs> menu)
        {
            this.menu = menu;
        }

        public async Task<IActionResult> Index(int idDelete)
        {
            var data = await menu.GetAllForAdminAsync();
            return View(data);
        }
    }
}
