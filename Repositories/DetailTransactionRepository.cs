using CSharp_VincentHadinata_2301850430.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CSharp_VincentHadinata_2301850430.Repositories
{
    class DetailTransactionRepository
    {
        public static int insert(DetailTransaction detail, int key)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VINCENT;Initial Catalog=marketDB;Integrated Security=True");
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;

            string query = "SELECT TOP 1 * FROM DetailTransaction ORDER BY TransactionID DESC";

            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;

            connection.Open();
            reader = command.ExecuteReader();

            //check result and process
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    detail.tr_id = Convert.ToInt32(reader["TransactionID"].ToString());
                }
            }
            else detail.tr_id = 1;

            if (key == 1)
            {
                detail.tr_id++;
            }

            reader.Close();

            command.Parameters.Add("@tr_id", SqlDbType.Int).Value = detail.tr_id;
            command.Parameters.Add("@prod_id", SqlDbType.Int).Value = detail.prod_id;
            command.Parameters.Add("@quantity", SqlDbType.Int).Value = detail.quantity;

            if (key == 1)
            {
                query = "INSERT INTO HeaderTransaction (TransactionID) VALUES (@tr_id)";
                command.CommandText = query;
                command.ExecuteNonQuery();
            }

            query = "INSERT INTO DetailTransaction VALUES (@tr_id, @prod_id, @quantity)";
            command.CommandText = query;

            //execute
            command.ExecuteNonQuery();

            query = "UPDATE Product SET quantity = quantity - @quantity WHERE ProductID = @prod_id";
            command.CommandText = query;
            command.ExecuteNonQuery();

            //Close connection
            connection.Close();
            command.Dispose();

            return detail.tr_id;
        }

        public static int totalPayment(int tr_id, string method)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VINCENT;Initial Catalog=marketDB;Integrated Security=True");
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;

            string query = "UPDATE HeaderTransaction SET PaymentMethod = @method WHERE TransactionID = @tr_id";
            command.Parameters.Add("@tr_id", SqlDbType.Int).Value = tr_id;
            command.Parameters.Add("@method", SqlDbType.VarChar).Value = method;

            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;

            connection.Open();
            command.ExecuteNonQuery();

            query = "SELECT Total = SUM(Price*dt.Quantity) FROM DetailTransaction dt JOIN Product pr ON pr.ProductID = dt.ProductID WHERE TransactionID = @tr_id GROUP BY TransactionID";
            command.CommandText = query;

            reader = command.ExecuteReader();

            //check result and process
            int total = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    total = Convert.ToInt32(reader["Total"]);
                }
            }

            reader.Close();
            connection.Close();
            command.Dispose();

            return total;
        }

        public static List<Product> viewTransactionDetail(int tr_id)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VINCENT;Initial Catalog=marketDB;Integrated Security=True");
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;

            List<Product> listProduct = new List<Product>();

            string query = "SELECT TransactionID,ProductName,dt.Quantity,Price FROM DetailTransaction dt JOIN Product pr ON pr.ProductID = dt.ProductID WHERE TransactionID = @tr_id";

            command.Parameters.Add("@tr_id", SqlDbType.Int).Value = tr_id;
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
    }
}
