using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projektos.Models
{
	public class SiteUser
	{
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Display(Name = "Nazwa użytkownika")]
		public string userName { get; set; }

		[Display(Name = "Hasło")]
		[DataType(DataType.Password)]
		public string password { get; set; }
	}
}
