using Api.Data.Interface;
using Api.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data.Repository
{
    public class ToDoRepository : IToDo
    {
        private static string ab = System.Reflection.Assembly.GetExecutingAssembly().Location;
        private static string ac = ab.Substring(0,ab.IndexOf("Api"));
        private static readonly string connectionStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ac + @"Api\Api.Data\Repository\ToDoDb.mdf;Integrated Security=True";
        SqlConnection con = new SqlConnection(connectionStr);

        public void CreateToDo(ToDo todo)
        {
            SqlCommand cmd = new SqlCommand("insert into [Table] (Title, Description, ExpireDate, CompletionPercentage) values (@title, @description, @expirydate, @completionpercentage)");
            
            cmd.Parameters.Add("@title", System.Data.SqlDbType.Char, 63).Value = todo.Title;
            cmd.Parameters.Add("@description", System.Data.SqlDbType.Char, 255).Value = todo.Description;
            cmd.Parameters.Add("@expirydate", System.Data.SqlDbType.DateTime).Value = todo.ExpiryDate;
            cmd.Parameters.Add("@completionpercentage", System.Data.SqlDbType.Int).Value = todo.CompletionPercent;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteToDo(int ToDoId)
        {
            SqlCommand cmd = new SqlCommand("delete from [Table] where Id = @todoid");

            cmd.Parameters.Add("@todoid", System.Data.SqlDbType.Int).Value = ToDoId;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<ToDo> GetAllToDos()
        {
            List<ToDo> res = new List<ToDo>();
            DataTable dt = new DataTable();

            string qr = "SELECT * FROM [Table]";
            SqlDataAdapter cmd = new SqlDataAdapter(qr, con);
            cmd.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ToDo prog = new ToDo
                {
                    ToDoId = Convert.ToInt32(dt.Rows[i]["Id"]),
                    Title = dt.Rows[i]["Title"].ToString(),
                    ExpiryDate = Convert.ToDateTime(dt.Rows[i]["ExpireDate"]),
                    Description = dt.Rows[i]["Description"].ToString(),
                    CompletionPercent = Convert.ToInt32(dt.Rows[i]["CompletionPercentage"])
                };

                res.Add(prog);
            }

            return res;
        }

        public ToDo GetToDo(int ToDoId)
        {
            ToDo res = new ToDo();
            SqlCommand cmd = new SqlCommand("select * from [Table] where Id = @todoid");

            cmd.Parameters.Add("@todoid", System.Data.SqlDbType.Int).Value = ToDoId;
            cmd.Connection = con;

            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    res = new ToDo
                    {
                        ToDoId = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString(),
                        ExpiryDate = Convert.ToDateTime(reader["ExpireDate"]),
                        Description = reader["Description"].ToString(),
                        CompletionPercent = Convert.ToInt32(reader["CompletionPercentage"])
                    };
                }
            }
            con.Close();

            return res;
        }

        public List<ToDo> GetToDoByDate(DateTime dt)
        {
            List<ToDo> res = new List<ToDo>();
            SqlCommand cmd = new SqlCommand("select * from [Table] where ExpireDate = @date");

            cmd.Parameters.Add("@date", System.Data.SqlDbType.DateTime).Value = dt.Day + "-" + dt.Month + "-" + dt.Year;
            cmd.Connection = con;

            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    res.Add(new ToDo
                    {
                        ToDoId = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString(),
                        ExpiryDate = Convert.ToDateTime(reader["ExpireDate"]),
                        Description = reader["Description"].ToString(),
                        CompletionPercent = Convert.ToInt32(reader["CompletionPercentage"])
                    });
                }
            }
            con.Close();

            return res;
        }

        public void MarkAsDone(int ToDoId)
        {
            SqlCommand cmd = new SqlCommand("update [Table] set CompletionPercentage = @per where Id = @todoid");

            cmd.Parameters.Add("@todoid", System.Data.SqlDbType.Int).Value = ToDoId;
            cmd.Parameters.Add("@per", System.Data.SqlDbType.Int).Value = 100;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void SetPercentage(int ToDoId, int percent)
        {
            SqlCommand cmd = new SqlCommand("update [Table] set CompletionPercentage = @percent where Id = @todoid");

            cmd.Parameters.Add("@todoid", System.Data.SqlDbType.Int).Value = ToDoId;
            cmd.Parameters.Add("@percent", System.Data.SqlDbType.Int).Value = percent;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateToDo(ToDo todo)
        {
            SqlCommand cmd = new SqlCommand("update [Table] set CompletionPercentage = @percent, Title = @title, Description = @desc, ExpireDate = @exdate where Id = @todoid");

            cmd.Parameters.Add("@todoid", System.Data.SqlDbType.Int).Value = todo.ToDoId;
            cmd.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 63).Value = todo.Title;
            cmd.Parameters.Add("@desc", System.Data.SqlDbType.VarChar, 255).Value = todo.Description;
            cmd.Parameters.Add("@exdate", System.Data.SqlDbType.DateTime).Value = todo.ExpiryDate;
            cmd.Parameters.Add("@percent", System.Data.SqlDbType.Int).Value = todo.CompletionPercent;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
