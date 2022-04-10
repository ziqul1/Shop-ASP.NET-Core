using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projektos.DAL;

namespace Projektos.Models
{
	public class MyPageModelCategories : MyPageModel
	{
        public MyPageModelCategories(IConfiguration configuration) : base(configuration)
        {

        }

        public List<Category> CategoryList { get; set; }

        public List<SelectListItem> CategorySelect { get; set; }

        public void LoadCategoryList()
        {
          //  CategoryList = categoryDB.List();
        }

        public void LoadCategorySelect()
        {
            if (CategoryList == null)
                LoadCategoryList();

            CategorySelect = CategoryList.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.LongName
                }).ToList();

            CategorySelect.Insert(0,
                new SelectListItem
                {
                    Value = "-1",
                    Text = ""
                });
        }

        public bool CheckCategoryExisting(int id)
        {
            if (CategoryList == null)
                LoadCategoryList();

            return CategoryList.Exists(c => c.Id == id);
        }

    }
}
