using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projektos.Utils
{
	public class CustomFilterAttributes : ResultFilterAttribute
	{
		public override void OnResultExecuting(ResultExecutingContext context)
		{
			var result = context.Result;

			if (result is PageResult)
			{
				var page = ((PageResult)result);
				page.ViewData["filterMessage"] = "Komunikat z CustomFilterAttributes!!!";
			}
		}
	}
}
