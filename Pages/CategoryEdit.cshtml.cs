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
    public class CategoryEditModel : MyPageModelCategories
    {
        public CategoryEditModel(IConfiguration configuration) : base(configuration)
        {
        }

        [BindProperty]
        public Category Category { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Category = categoryDB.GetItem((int)id);

            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //categoryDB.Update(Category);
            return RedirectToPage("CategoryList");
        }
    }
}
