using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projektos.Models;
using System.Data;
using System.Data.SqlClient;

namespace Projektos.DAL
{
	public class ProductDB
	{
        private string connectionString;

        public ProductDB(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Product> List(int categoryId = -1)
        {
            List<Product> products;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                products = new List<Product>();

                SqlCommand cmd = new SqlCommand("sp_productSelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        products.Add(createProduct(reader));
                }
                catch (Exception)
                {
                    throw new ApplicationException("Data error.");
                }

            }

            if (categoryId != -1)
                return products.Where(p => p.CategoryId == categoryId).ToList();
            else 
                return products;
        }

        public int Insert(Product p)
        {
            int id = -1;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_productAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    setParameters(cmd, p);
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    id = (int)cmd.Parameters["@ProductId"].Value;
                }
                catch (Exception)
                {
                    throw new ApplicationException("Data error.");
                }
            }
            return id;
        }

        public void Update(Product p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_productUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = p.Id;
                    setParameters(cmd, p);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException("Data error.");
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_productDelete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = id;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException("Data error.");
                }
            }
        }
        public Product GetItem(int id)
        {
            Product product;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_productGetItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = id;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    product = createProduct(reader);
                }
                catch (Exception)
                {
                    throw new ApplicationException("Data error.");
                }
            }
            return product;
        }

        private Product createProduct(SqlDataReader reader)
        {
            try
            {
                int.TryParse(reader["Id"].ToString(), out int _id);
                decimal.TryParse(reader["Price"].ToString(), out decimal _price);
                int.TryParse(reader["CategoryId"].ToString(), out int _categoryId);
                return new Product
                {
                    Id = _id,
                    Name = reader["Name"].ToString(),
                    Price = _price,
                    CategoryId = _categoryId
                };
            }
            catch (Exception)
            {
                throw new ApplicationException("Data error.");
            }
        }

        private void setParameters(SqlCommand cmd, Product p)
        {
            cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = p.Name;
            cmd.Parameters.Add("@Price", SqlDbType.Money).Value = p.Price;
            cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = p.CategoryId;
        }
    }
}
