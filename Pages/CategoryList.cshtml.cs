using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Projektos.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Projektos.Pages
{
    public class CategoryListModel : MyPageModelCategories
    {
        public CategoryListModel(IConfiguration configuration) : base(configuration)
        {
        }

        public void OnGet()
        {
            var SessionAddress = HttpContext.Session.GetString("SessionAddress");

            if (SessionAddress != null)
                CategoryList = JsonConvert.DeserializeObject<List<Category>>(SessionAddress);

            LoadCategoryList();
        }
    }
}
