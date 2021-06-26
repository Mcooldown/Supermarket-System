using CSharp_VincentHadinata_2301850430.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CSharp_VincentHadinata_2301850430.Repositories
{
    class ProductRepository
    {
        public static List<Product> view()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VINCENT;Initial Catalog=marketDB;Integrated Security=True");
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;

            List<Product> listProduct = new List<Product>();

            string query = "SELECT * FROM Product";

            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;

            connection.Open();

            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Product product = new Product();
                    product.id = Convert.ToInt32(reader["ProductID"].ToString());
                    product.name = reader["ProductName"].ToString();
                    product.quantity = Convert.ToInt32(reader["Quantity"].ToString());
                    product.price = Convert.ToInt32(reader["Price"].ToString());
                    listProduct.Add(product);
                }
            }

            reader.Close();
            connection.Close();
            command.Dispose();

            return listProduct;
        }

        public static Product search(int id, int key)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VINCENT;Initial Catalog=marketDB;Integrated Security=True");
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;

            string query = null;
            if(key ==1)
            {
                query = "SELECT TOP 1 * FROM Product WHERE ProductID = @id";
            }else if (key == 2)
            {
                query = "SELECT TOP 1 * FROM Product ORDER BY ProductID DESC";
            }
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;

            connection.Open();

            reader = command.ExecuteReader();

            Product product = new Product();
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    product.id = Convert.ToInt32(reader["ProductID"].ToString());
                    product.name = reader["ProductName"].ToString();
                    product.quantity = Convert.ToInt32(reader["Quantity"].ToString());
                    product.price = Convert.ToInt32(reader["Price"].ToString());
                }
            }

            reader.Close();
            connection.Close();
            command.Dispose();

            return product;
        }

        public static void insert(Product product)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VINCENT;Initial Catalog=marketDB;Integrated Security=True");
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;

            connection.Open();

            Product check = search(0, 2);

            if(check == null)
            {
                product.id = 1;
            }
            else
            {
                product.id = check.id + 1;
            }

            string query = "INSERT INTO Product VALUES (@id, @name, @quantity,@price)";
            command.Parameters.Add("@id", SqlDbType.Int).Value = product.id;
            command.Parameters.Add("@name", SqlDbType.VarChar).Value = product.name;
            command.Parameters.Add("@quantity", SqlDbType.VarChar).Value = product.quantity;
            command.Parameters.Add("@price", SqlDbType.VarChar).Value = product.price;

            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;

            command.ExecuteNonQuery();

            connection.Close();
            command.Dispose();
        }

        public static void update(Product product)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VINCENT;Initial Catalog=marketDB;Integrated Security=True");
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;

            connection.Open();

            string query = "UPDATE Product SET ProductName = @name, Quantity = @quantity, Price = @price WHERE ProductID = @id";
            command.Parameters.Add("@id", SqlDbType.Int).Value = product.id;
            command.Parameters.Add("@name", SqlDbType.VarChar).Value = product.name;
            command.Parameters.Add("@quantity", SqlDbType.VarChar).Value = product.quantity;
            command.Parameters.Add("@price", SqlDbType.VarChar).Value = product.price;

            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;

            command.ExecuteNonQuery();

            connection.Close();
            command.Dispose();
        }

        public static void delete(int delete_id)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VINCENT;Initial Catalog=marketDB;Integrated Security=True");
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;

            connection.Open();

            string query = "DELETE FROM Product WHERE ProductID = @id";
            command.Parameters.Add("@id", SqlDbType.Int).Value = delete_id;

            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;

            command.ExecuteNonQuery();

            connection.Close();
            command.Dispose();
        }
    }
}
