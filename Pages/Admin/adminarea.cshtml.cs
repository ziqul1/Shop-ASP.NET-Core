using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Projektos.DAL;
using Projektos.Models;

namespace Projektos.Pages.Admin
{
    public class adminareaModel : MyPageModelCategories
    {
		public List<Product> ProductList { get; set; }

		[BindProperty]
		public int CategoryId { get; set; }

		public adminareaModel(IConfiguration configuration) : base(configuration)
		{
		}

		public IActionResult OnGet(int categoryId)
		{
			LoadCategoryList();
			LoadCategorySelect();
			if (!CheckCategoryExisting(categoryId))
				categoryId = -1;

			ProductList = productDB.List(categoryId);
			return Page();
		}

		public IActionResult OnPost()
		{
			return OnGet(CategoryId);
		}
	}
}
