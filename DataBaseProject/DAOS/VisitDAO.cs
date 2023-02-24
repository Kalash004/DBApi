using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseProject.DBEntities;
using DataBaseProject.Interfaces;

namespace DataBaseProject.DAOS
{
    internal class VisitDAO : IDAO<Visit>
    {
        private static String table = "Visits";
        private String C_SAVE = String.Format("INSERT INTO {0} (userId, visit_date) VALUES (@userId, @visit_date)", table);
        private String C_UPDATE = String.Format("UPDATE {0} SET userId = @userId, visit_date = @visit_date WHERE id = @id", table);
        private String C_READ_ALL = String.Format("SELECT * FROM {0}", table);
        private String C_READ_BY_ID = String.Format("SELECT * FROM {0} WHERE id = @id", table);
        private String C_DELETE = String.Format("DELETE FROM {0} WHERE id = @id", table);

        public void Delete(Visit element)
        {
            SqlConnection conn = DataBaseSingleton.GetInstance();
            using (SqlCommand command = new SqlCommand(C_DELETE, conn))
            {
                command.Parameters.Add(new SqlParameter("@id", element.ID));
                command.ExecuteNonQuery();
                element.ID = -1;
            }
        }

        public IEnumerable<Visit> GetAll()
        {
            SqlConnection conn = DataBaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand(C_READ_ALL, conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Visit visit = new Visit(
                        Convert.ToInt32(reader[0].ToString()),
                        new UserDAO().GetByID(Convert.ToInt32(reader[1].ToString())), // Not sure if this is the right way of handling references
                        Convert.ToDateTime(reader[2].ToString())
                    );
                    yield return visit;
                }
                reader.Close();
            }
        }

        public Visit? GetByID(int id)
        {
            SqlConnection conn = DataBaseSingleton.GetInstance();
            Visit? visit = null;

            using (SqlCommand command = new SqlCommand(C_READ_BY_ID, conn))
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    visit = new Visit(
                        Convert.ToInt32(reader[0].ToString()),
                        new UserDAO().GetByID(Convert.ToInt32(reader[1].ToString())), // Not sure if this is the right way of handling references
                        Convert.ToDateTime(reader[2].ToString())
                        );
                }
                reader.Close();
            }
            return visit;
        }

        public void Save(Visit element)
        {
            SqlConnection conn = DataBaseSingleton.GetInstance();

            SqlCommand command = null;

            if (element.ID < 0)
            {
                using (command = new SqlCommand(C_SAVE, conn))
                {
                    command.Parameters.Add(new SqlParameter("@userId", element.User.ID));
                    command.Parameters.Add(new SqlParameter("@visit_date", element.SqlTime));
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
                    command.Parameters.Add(new SqlParameter("@userId", element.User.ID));
                    command.Parameters.Add(new SqlParameter("@visit_date", element.SqlTime));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
