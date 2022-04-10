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
    public class DeleteModel : MyPageModel
    {
        public DeleteModel(IConfiguration configuration) : base(configuration)
        {
        }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        [BindProperty]
		public Product Product { get; set; }

		public IActionResult OnGet(int? id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            Product = productDB.GetItem((int)id);

            return Page();
        }

        public IActionResult OnPost()
        {
            if (Id != null)
                productDB.Delete((int)Id);
            return RedirectToPage("Index");
        }
    }
}
