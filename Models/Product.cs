using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projektos.Models
{
	public class Product
	{
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Id jest wymagane.")]
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        public string Name { get; set; }

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Cena jest wymagana.")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Cena nie może byc ujemna.")]
        public decimal Price { get; set; }

        [Display(Name = "Kategoria")]
        [Required(ErrorMessage = "Okreslenie kategorii jest wymagane.")]
        [Range(1, int.MaxValue, ErrorMessage = "Okreslenie kategorii jest wymagane.")]
        public int CategoryId { get; set; }
    }
}
