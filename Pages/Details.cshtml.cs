using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Projektos.Models;
using Projektos.DAL;

namespace Projektos.Pages
{
    public class DetailsModel : MyPageModel2
    {
        
        public DetailsModel(IConfiguration configuration, IProductDB _productDB) : base(configuration, _productDB)
        {
        }
        
        [BindProperty]
        public Product Product { get; set; }

        public Category Category { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = productDB2.Get((int)id);

            if (Product == null)
            {
                return NotFound();
            }

           // Category = categoryDB.GetItem(productDB.GetItem((int)id).CategoryId);

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            LoadCart();
            cart.AddItem(productDB.GetItem((int)id).Id);
            SaveCart();

            return RedirectToPage("Cart");
        }
    }
}
