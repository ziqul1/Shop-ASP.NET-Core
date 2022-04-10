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
    public class EditModel : MyPageModelCategories
    {
        public EditModel(IConfiguration configuration) : base(configuration)
        {
        }

        [BindProperty]
        public Product product { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            product = productDB.GetItem((int)id);

            if (product == null)
            {
                return NotFound();
            }

            LoadCategorySelect();

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            productDB.Update(product);
            return RedirectToPage("Index");
        }
    }
}
