using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projektos.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Projektos.Pages
{
	public class IndexModel : MyPageModelCategories
	{

		public List<Product> ProductList { get; set; }

		[BindProperty]
		public int CategoryId { get; set; }

		public IndexModel(IConfiguration configuration) : base(configuration)
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
			
			if (ModelState.IsValid)
			{
				HttpContext.Session.SetString("SessionAddress", JsonConvert.SerializeObject(CategoryList));
				return OnGet(CategoryId);
			}
			return Page();
		}
	}
}
