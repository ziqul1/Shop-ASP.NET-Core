using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projektos.Models;
using Microsoft.Extensions.Configuration;
using Projektos.DAL;


namespace Projektos.Pages
{
    public class CreateModel : MyPageModelCategories
    {
        public CreateModel(IConfiguration configuration) : base(configuration)
        {
        }

        [BindProperty]
        public Product newProduct { get; set; }

        public string FailMessage { get; set; }


        public IActionResult OnGet(string msg = null)
        {
            if (msg != null) FailMessage = msg;

            LoadCategorySelect();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return OnGet();
            }
            if (!CheckCategoryExisting(newProduct.CategoryId))
            {
                return OnGet("Podana kategoria nie istnieje!");
            }

            productDB.Insert(newProduct);
            return RedirectToPage("Index");
        }
    }
}
