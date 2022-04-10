using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Projektos.Utils
{
	public class CustomPageFilter : IPageFilter
	{
		private IConfiguration config;

		public CustomPageFilter(IConfiguration _config)
		{
			this.config = _config;
		}

		public void OnPageHandlerSelected(PageHandlerSelectedContext pageContext)
		{
		}

		public void OnPageHandlerExecuting(PageHandlerExecutingContext pageContext)
		{
		}

		public void OnPageHandlerExecuted(PageHandlerExecutedContext pageContext)
		{
			var page = (PageResult)pageContext.Result;
			page.ViewData["NameOfCompany"] = config.GetValue<string>("Company:NameOfCompany");
		}
	}
}
