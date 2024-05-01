using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfilo.Areas.Admin.ViewModels;

namespace Portfilo.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(Register collection)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", "Please fill all required fields!");
					return View(collection);
				}

				var user = new IdentityUser
				{
					UserName = collection.Email,
					Email = collection.Email
				};

				var signIn = await userManager.CreateAsync(user, collection.Password);

				if (signIn.Succeeded)
				{
					await signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("",signIn.Errors.ToString()!);
				return View(collection);
			}
			catch
			{

				return View(collection);
			}
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(Login collection)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("","Please fill all required fields!");
					return View(collection);
				}

				var logIn = await signInManager.PasswordSignInAsync(collection.Email, collection.Password, isPersistent:collection.RememberMe, lockoutOnFailure:false);
				if (logIn.Succeeded)
				{
					return RedirectToAction("Index","Home");
				}

				ModelState.AddModelError("","Wronge email or password!");
				return View(collection);
			}
			catch
			{

				throw;
			}
		}

		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
	}
}
