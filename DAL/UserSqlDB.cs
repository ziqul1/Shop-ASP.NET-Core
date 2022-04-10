using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Projektos.Models;
using System.Data;
using System.Data.SqlClient;

namespace Projektos.DAL
{
	public class UserSqlDB : IUserDB
	{
		private string connectionString;

		public UserSqlDB(IConfiguration configuration)
		{
			connectionString = configuration.GetConnectionString("AlcarDB");
		}

		private SiteUser createUser(SqlDataReader reader)
		{
			int.TryParse(reader["Id"].ToString(), out int _id);
			return new SiteUser
			{
				Id = _id,
				userName = reader["Login"].ToString(),
				password = reader["Password"].ToString()
			};
		}

		public async Task<List<SiteUser>> List()
		{
			var users = new List<SiteUser>();

			using (var con = new SqlConnection(connectionString))
			{
				try
				{
					var cmd = new SqlCommand("usersSelectAll", con);
					cmd.CommandType = CommandType.StoredProcedure;
					await con.OpenAsync();
					var reader = await cmd.ExecuteReaderAsync();
					while (await reader.ReadAsync())
						users.Add(createUser(reader));
				}
				catch (Exception)
				{
					throw new ApplicationException("Data error.");
				}
			}
			return users;
		}


		public async Task Add(SiteUser user)
		{
			using (var con = new SqlConnection(connectionString))
			{
				try
				{
					string procedureName = "userAdd";
					var cmd = new SqlCommand(procedureName, con);

					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@Login", SqlDbType.VarChar, 30).Value = user.userName;
					cmd.Parameters.Add("@Password", SqlDbType.VarChar, 90).Value = user.password;
					cmd.Parameters.Add("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;
					await con.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
				}
				catch (Exception e)
				{
					string msg = e.Message;
				}
			}
		}

		public async Task<SiteUser> Get(string name)
		{
			SiteUser user = null;

			using (var con = new SqlConnection(connectionString))
			{
				try
				{
					var cmd = new SqlCommand("userGetByName", con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@Login", SqlDbType.VarChar, 90).Value = name;
					await con.OpenAsync();
					var reader = await cmd.ExecuteReaderAsync();
					await reader.ReadAsync();
					user = createUser(reader);
				}
				catch (Exception e)
				{
					string msg = e.Message;
				}
			}
			return user;
		}
	}
}
