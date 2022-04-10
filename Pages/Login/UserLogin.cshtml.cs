using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projektos.Models;
using Projektos.DAL;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Projektos.Pages.Login
{
    public class UserLoginModel : PageModel
    {
		private IUserDB userDB;

		public UserLoginModel(IUserDB userDB) => this.userDB = userDB;

		private readonly IConfiguration _configuration;

		public string Message { get; set; }

		[BindProperty]
		public SiteUser user { get; set; }


		private async Task<bool> validateUser()
		{
			var loadedUser = await userDB.Get(user.userName);

			var hasher = new PasswordHasher<string>();

			if(loadedUser != null && hasher.VerifyHashedPassword(null, loadedUser.password, 
				user.password) == PasswordVerificationResult.Success)
			{
				user = loadedUser;
				return true;
			}
			else
			{
				ViewData["ErrorMessage"] = "Nieprawid³owy login lub has³o";
				return false;
			}

		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			if (ModelState.IsValid && await validateUser())
			{
				var claims = new List<Claim>()
				{
					new Claim(ClaimTypes.Name, user.userName),
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
				};

				var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
				await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
				return RedirectToPage(returnUrl);
			}
			return Page();
		}

		public void OnGet() { }
	}
}
