using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Projektos.Models;

namespace Projektos.Pages
{
    public class CategoryCreateModel : MyPageModel
    {
        public CategoryCreateModel(IConfiguration configuration) : base(configuration)
        {
        }

        [BindProperty]
        public Category NewCategory { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //categoryDB.Insert(NewCategory);
            return RedirectToPage("CategoryList");
        }
    }
}
