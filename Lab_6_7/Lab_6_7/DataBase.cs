using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_6_7
{
    public class DataBase : IDisposable
    {
        string connectionString;
        SqlConnection connection = null;

        public DataBase()
        {
            connectionString = "server=DESKTOP-23I014S;Trusted_Connection=Yes;DataBase=CW_PartShop;";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void Dispose()
        {
            if (connection != null)
                connection.Close();
        }

        public bool AddPart(Part p)
        {
            string sql = $"INSERT INTO PART(NAME, DESCRIPTION, QUANTITY, PRICE, ImagePath) VALUES " +
                $"(\'{p.Name}\', \'{p.Description}\', \'{p.Quantity}\', \'{p.Price}\', \'{p.ImagePath}\')";
            try
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public List<Part> GetParts()
        {
            string sql = "SELECT * FROM PART";
            DataTable partsTable = new DataTable();
            SqlDataAdapter adapter;

            List<Part> parts = new List<Part>();
            try
            {
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(partsTable);
                for(int i = 0; i < partsTable.Rows.Count; i++)
                {
                    Part part = new Part(partsTable.Rows[i].Field<int>("ID"),
                        partsTable.Rows[i].Field<string>("NAME"),
                        partsTable.Rows[i].Field<string>("DESCRIPTION"),
                        partsTable.Rows[i].Field<int>("QUANTITY"),
                        partsTable.Rows[i].Field<int>("PRICE"),
                        partsTable.Rows[i].Field<string>("ImagePath"));
                    parts.Add(part);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return parts;
        }
        public bool Update(int id, Part p)
        {
            string sql = $"UPDATE PART SET NAME=\'{p.Name}\', DESCRIPTION = \'{p.Description}\', QUANTITY = \'{p.Quantity}\', PRICE = \'{p.Price}\', ImagePath = \'{p.ImagePath}\' WHERE ID = {id}";
            try
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool Delete(Part d)
        {
            string sql = $"DELETE FROM PART WHERE ID = {d.Id}";

            try
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
