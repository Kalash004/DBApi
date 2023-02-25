using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataBaseProject.DBEntities;
using DataBaseProject.Interfaces;

namespace DataBaseProject.DAOS
{
    internal class UserDAOImpl : AbstractDAO<User>, IDAO<User>
    {
        private static String table = "Users";
        private String C_CREATE = String.Format("INSERT INTO {0} (user_id, u_name, u_surname, total_spent) VALUES (@user_id, @u_name, @u_surname, @total_spent)", table);
        private String C_UPDATE = String.Format("UPDATE {0} SET user_id = @user_id, u_name = @u_name, u_surname = @u_surname, total_spent = @total_spent WHERE id = @id", table);
        private String C_READ_ALL = String.Format("SELECT * FROM {0}", table);
        private String C_READ_BY_ID = String.Format("SELECT * FROM {0} WHERE id = @id", table);
        private String C_DELETE = String.Format("DELETE FROM {0} WHERE id = @id", table);

        public int Create(User element)
        {
            return Create(C_CREATE, element);
        }

        public void Delete(int id)
        {
            Delete(C_DELETE, id);
        }

        public List<User> GetAll()
        {
            return GetAll(C_READ_ALL);
        }

        public User? GetByID(int id)
        {
            return GetByID(C_READ_BY_ID, id);
        }

        public User? GetByIDWithVisits(int id)
        {
            User user = GetByID(id);
            if (user != null)
            {
                VisitDAOImpl visitDAO = new VisitDAOImpl();
                var user_visits = visitDAO.GetUserVisits(id);
                user.Visits = user_visits;
            }
            return user;
        }

        public void Save(User element)
        {
            Update(C_UPDATE, element, element.ID);
        }

        protected override User Map(SqlDataReader reader)
        {
            return new User(
                     Convert.ToInt32(reader[0].ToString()),
                     Convert.ToInt32(reader[1].ToString()),
                     reader[2].ToString(),
                     reader[3].ToString(),
                     Convert.ToInt32(reader[4].ToString())
                     );

        }

        protected override List<SqlParameter> Map(User obj)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@user_id", obj.User_id),
                new SqlParameter("@u_name", obj.Name),
                new SqlParameter("@u_surname", obj.Surname),
                new SqlParameter("@total_spent", obj.Total_spent)
            };
        }
    }
}
