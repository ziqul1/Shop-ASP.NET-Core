using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projektos.DAL;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Projektos.Models
{
	public class MyPageModel : PageModel
	{
		public IProductDB productDB2;

		/*
		public MyPageModel(IProductDB _productDB)
		{
			productDB2 = _productDB;
		}
		*/

		public ProductDB productDB;

		//public CategoryDB categoryDB;

		public Cart cart;

		public string connectionString { get; set; }

		public IConfiguration _configuration { get; }

		private static string CartCookieName = "CartCookie";

		public MyPageModel(IConfiguration configuration)
		{
			_configuration = configuration;
			productDB = new ProductDB(_configuration.GetConnectionString("AlcarDB"));
			//categoryDB = new CategoryDB(_configuration.GetConnectionString("AlcarDB"));
			cart = new Cart();
		}

		public void LoadCart()
		{
			string CartCookie = Request.Cookies[CartCookieName];
			cart.Load(CartCookie);
		}

		public void SaveCart()
		{
			Response.Cookies.Append(CartCookieName, cart.Save());
		}
	}
}
