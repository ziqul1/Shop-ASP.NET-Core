using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projektos.Models
{
	public class Category
	{
		[Display(Name = "Id")]
		[Required(ErrorMessage = "Id jest wymagane.")]
		public int Id { get; set; }

		[Display(Name = "Krotka nazwa")]
		[Required(ErrorMessage = "Nazwa jest wymagana.")]
		public string ShortName { get; set; }

		[Display(Name = "Nazwa")]
		[Required(ErrorMessage = "Nazwa jest wymagana.")]
		public string LongName { get; set; }
	}
}
