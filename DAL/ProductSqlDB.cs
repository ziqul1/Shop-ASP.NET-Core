using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Projektos.Models;

namespace Projektos.DAL
{
	public class ProductSqlDB : IProductDB
	{
		private string connectionString;

		public ProductSqlDB(IConfiguration configuration)
		{
			connectionString = configuration.GetConnectionString("AlcarDB");
		}

		private Product createProduct(SqlDataReader reader)
		{
			int.TryParse(reader["Id"].ToString(), out int _id);
			decimal.TryParse(reader["Price"].ToString(), out decimal _price);
			return new Product
			{
				Id = _id,
				Name = reader["Name"].ToString(),
				Price = _price,
			};
		}

		// prawdopodobnie nie bedzie potrzebne, sprawdzic
		public List<Product> List()
		{
			List<Product> products;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				products = new List<Product>();

				var cmd = new SqlCommand("productSelectAll", con);
				cmd.CommandType = CommandType.StoredProcedure;

				con.Open();
				var reader = cmd.ExecuteReader();
				while (reader.Read())
					products.Add(createProduct(reader));
			}
			return products;
		}

		public Product Get(int _id)
		{
			Product product;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("sp_productGetItem", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = _id;
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				reader.Read();
				product = createProduct(reader);
			}
			return product;
		}
	}
}
