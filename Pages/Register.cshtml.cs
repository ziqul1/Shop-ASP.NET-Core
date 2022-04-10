using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projektos.DAL;
using Projektos.Models;

namespace Projektos.Pages
{
    public class RegisterModel : PageModel
    {
        private IUserDB userDB;

		public RegisterModel(IUserDB userDB) => this.userDB = userDB;

        [BindProperty]
        public SiteUser newUser { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid && await validateUser())
			{
                var hasher = new PasswordHasher<string>();

                newUser.password = hasher.HashPassword(null, newUser.password);
                await userDB.Add(newUser);
                return RedirectToPage("Index");
			}
            return Page();
        }

        private async Task<bool> validateUser()
		{
            var loadedUser = await userDB.Get(newUser.userName);
            return loadedUser == null;
		}
    }
}
