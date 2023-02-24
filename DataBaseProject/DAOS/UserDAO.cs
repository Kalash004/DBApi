using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataBaseProject.DBEntities;
using DataBaseProject.Interfaces;

namespace DataBaseProject.DAOS
{
    internal class UserDAO : IDAO<User>
    {
        private static String table = "Users";
        private String C_SAVE = String.Format("INSERT INTO {0} (user_id, u_name, u_surname, total_spent) VALUES (@user_id, @u_name, @u_surname, @total_spent)", table);
        private String C_UPDATE = String.Format("UPDATE {0} SET user_id = @user_id, u_name = @u_name, u_surname = @u_surname, total_spent = @total_spent WHERE id = @id", table);
        private String C_READ_ALL = String.Format("SELECT * FROM {0}", table);
        private String C_READ_BY_ID = String.Format("SELECT * FROM {0} WHERE id = @id", table);
        private String C_DELETE = String.Format("DELETE FROM {0} WHERE id = @id", table);

        public void Delete(User element)
        {
            SqlConnection conn = DataBaseSingleton.GetInstance();
            using (SqlCommand command = new SqlCommand(C_DELETE, conn))
            {
                command.Parameters.Add(new SqlParameter("@id", element.ID));
                command.ExecuteNonQuery();
                element.ID = -1;
            }
        }

        public IEnumerable<User> GetAll()
        {
            SqlConnection conn = DataBaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand(C_READ_ALL, conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User(
                        Convert.ToInt32(reader[0].ToString()),
                        Convert.ToInt32(reader[1].ToString()),
                        reader[2].ToString(),
                        reader[3].ToString(),
                        Convert.ToInt32(reader[4].ToString())
                    );
                    yield return user;
                }
                reader.Close();
            }
        }

        public User? GetByID(int id)
        {
            SqlConnection conn = DataBaseSingleton.GetInstance();
            User? user = null;
            using (SqlCommand command = new SqlCommand(C_READ_BY_ID, conn))
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user = new User(
                       Convert.ToInt32(reader[0].ToString()),
                       Convert.ToInt32(reader[1].ToString()),
                       reader[2].ToString(),
                       reader[3].ToString(),
                       Convert.ToInt32(reader[4].ToString())
                       );
                }
                reader.Close();
            }
            return user;
        }

        public void Save(User element)
        {
            SqlConnection conn = DataBaseSingleton.GetInstance();

            SqlCommand command = null;

            if (element.ID < 0)
            {
                using (command = new SqlCommand(C_SAVE, conn))
                {
                    command.Parameters.Add(new SqlParameter("@user_id", element.User_id));
                    command.Parameters.Add(new SqlParameter("@u_name", element.Name));
                    command.Parameters.Add(new SqlParameter("@u_surname", element.Surname));
                    command.Parameters.Add(new SqlParameter("@total_spent", element.Total_spent));
                    command.ExecuteNonQuery();
                    command.CommandText = "Select @@Identity";
                    element.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand(C_UPDATE, conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", element.ID));
                    command.Parameters.Add(new SqlParameter("@user_id", element.User_id));
                    command.Parameters.Add(new SqlParameter("@u_name", element.Name));
                    command.Parameters.Add(new SqlParameter("@u_surname", element.Surname));
                    command.Parameters.Add(new SqlParameter("@total_spent", element.Total_spent));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

