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
    public class CartModel : MyPageModel
    {
        public List<Tuple<Product, int>> productList;

        [BindProperty]
        public int Id { get; set; }

        public bool objectNotExist;

        public CartModel(IConfiguration configuration) : base(configuration)
        {
        }

        public void OnGet()
        {
            LoadCart();
            objectNotExist = false;

            if (!cart.Empty())
            {
                Product product;
                productList = new List<Tuple<Product, int>>();

                for (int i = 0; i < cart.IdList.Count; i++)
                {
                    product = productDB.List().FirstOrDefault(p => p.Id == cart.IdList[i]);
                    if (product == null)
                        objectNotExist = true;
                    else
                        productList.Add(Tuple.Create(product, cart.Numbers[i]));
                }
            }
        }

        public IActionResult OnGetAdd(int id)
        {
            LoadCart();
            cart.AddItem(id);
            SaveCart();

            return RedirectToPage("Cart");
        }

        public IActionResult OnGetDecrease(int id)
        {
            LoadCart();
            cart.DecreaseItem(id);
            SaveCart();

            return RedirectToPage("Cart");
        }

        public IActionResult OnGetDelete(int id)
        {
            LoadCart();
            cart.DeleteItem(id);
            SaveCart();

            return RedirectToPage("Cart");
        }

        public void OnPostClear()
        {
            LoadCart();

            if (!cart.Empty())
            {
                cart.Clear();
                SaveCart();
            }
        }
    }
}
