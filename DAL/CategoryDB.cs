//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Data;
//using System.Data.SqlClient;
//using Projektos.Models;

//namespace Projektos.DAL
//{
//	public class CategoryDB
//	{
//		private string connectionString;

//		public CategoryDB(string connectionString)
//		{
//			this.connectionString = connectionString;
//		}
		
//		public List<Category> List()
//		{
//			List<Category> categories;

//			using (SqlConnection con = new SqlConnection(connectionString))
//			{
//				try
//				{
//					categories = new List<Category>();

//					SqlCommand cmd = new SqlCommand("sp_categorySelectAll", con);
//					cmd.CommandType = CommandType.StoredProcedure;
//					con.Open();
//					SqlDataReader reader = cmd.ExecuteReader();

//					while (reader.Read())
//						categories.Add(createCategory(reader));
//				}
//				catch (Exception)
//				{
//					throw new ApplicationException("Data error.");
//				}
//			}
//			return categories;
//		}

//		public int Insert(Category c)
//		{
//			int id = -1;
//			using (SqlConnection con = new SqlConnection(connectionString))
//			{
//				try
//				{
//					SqlCommand cmd = new SqlCommand("sp_categoryAdd", con);
//					cmd.CommandType = CommandType.StoredProcedure;
//					cmd.Parameters.Add("@ShortName", SqlDbType.VarChar, 50).Value = c.ShortName;
//					cmd.Parameters.Add("@LongName", SqlDbType.VarChar, 50).Value = c.LongName;
//					cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Direction = ParameterDirection.Output;
//					con.Open();
//					cmd.ExecuteNonQuery();
//					id = (int)cmd.Parameters["@CategoryId"].Value;
//				}
//				catch (Exception)
//				{
//					throw new ApplicationException("Data error.");
//				}
//			}

//			return id;
//		}

//		public void Update(Category c)
//		{
//			using (SqlConnection con = new SqlConnection(connectionString))
//			{
//				try
//				{
//					SqlCommand cmd = new SqlCommand("sp_categoryUpdate", con);
//					cmd.CommandType = CommandType.StoredProcedure;
//					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = c.Id;
//					cmd.Parameters.Add("@ShortName", SqlDbType.VarChar, 50).Value = c.ShortName;
//					cmd.Parameters.Add("@LongName", SqlDbType.VarChar, 50).Value = c.LongName;
//					con.Open();
//					cmd.ExecuteNonQuery();
//				}
//				catch (Exception)
//				{
//					throw new ApplicationException("Data error.");
//				}
//			}
//		}

//		public void Delete(int id)
//		{
//			using (SqlConnection con = new SqlConnection(connectionString))
//			{
//				try
//				{
//					SqlCommand cmd = new SqlCommand("sp_categoryDelete", con);
//					cmd.CommandType = CommandType.StoredProcedure;
//					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = id;
//					con.Open();
//					cmd.ExecuteNonQuery();
//				}
//				catch (Exception)
//				{
//					throw new ApplicationException("Data error.");
//				}
//			}
//		}
//		public Category GetItem(int id)
//		{
//			Category category;
//			using (SqlConnection con = new SqlConnection(connectionString))
//			{
//				try
//				{
//					SqlCommand cmd = new SqlCommand("sp_categoryGetItem", con);
//					cmd.CommandType = CommandType.StoredProcedure;
//					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = id;
//					con.Open();
//					SqlDataReader reader = cmd.ExecuteReader();
//					reader.Read();
//					category = createCategory(reader);
//				}
//				catch (Exception)
//				{
//					throw new ApplicationException("Data error.");
//				}
//			}
//			return category;
//		}

//		private Category createCategory(SqlDataReader reader)
//		{
//			int.TryParse(reader["Id"].ToString(), out int _id);
//			return new Category
//			{
//				Id = _id,
//				ShortName = reader["ShortName"].ToString(),
//				LongName = reader["LongName"].ToString()
//			};
//		}
//	}
//}
