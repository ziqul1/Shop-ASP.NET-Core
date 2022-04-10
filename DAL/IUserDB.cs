using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projektos.Models;

namespace Projektos.DAL
{
	public interface IUserDB
	{
		public Task Add(SiteUser user);
		public Task<SiteUser> Get(string name);
		public Task<List<SiteUser>> List();

	}
}

