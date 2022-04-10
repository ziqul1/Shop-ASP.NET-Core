using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projektos.Models;
using Microsoft.Extensions.Configuration;

namespace Projektos.Pages
{
    public class CategoryDeleteModel : MyPageModelCategories
    {
        public CategoryDeleteModel(IConfiguration configuration) : base(configuration)
        {
        }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        public IActionResult OnGet()
        {
            if (Id == null)
            {
                return NotFound();
            }

            LoadCategoryList();

            return Page();
        }

        //public IActionResult OnPost()
        //{
        //    if (Id != null)
        //        //categoryDB.Delete((int)Id);
        //    return RedirectToPage("CategoryList");
        //}
    }
}
