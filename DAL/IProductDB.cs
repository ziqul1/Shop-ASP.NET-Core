using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projektos.Models;

namespace Projektos.DAL
{
	public interface IProductDB
	{
		public List<Product> List();
		public Product Get(int _id);
	}
}
