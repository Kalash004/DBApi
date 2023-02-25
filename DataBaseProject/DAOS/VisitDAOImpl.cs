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
    internal class VisitDAOImpl : AbstractDAO<Visit>, IDAO<Visit>
    {
        private static String table = "Visits";
        private String C_CREATE = String.Format("INSERT INTO {0} (userId, visit_date) VALUES (@userId, @visit_date)", table);
        private String C_UPDATE = String.Format("UPDATE {0} SET userId = @userId, visit_date = @visit_date WHERE id = @id", table);
        private String C_READ_ALL = String.Format("SELECT * FROM {0}", table);
        private String C_READ_BY_ID = String.Format("SELECT * FROM {0} WHERE id = @id", table);
        private String C_DELETE = String.Format("DELETE FROM {0} WHERE id = @id", table);
        private String C_READ_BY_USER_ID = String.Format("SELECT * FROM {0} WHERE userId = @id", table);

        public int Create(Visit element)
        {
            return Create(C_CREATE, element);
        }

        public void Delete(int id)
        {
            Delete(C_DELETE, id);
        }

        public List<Visit> GetAll()
        {
            return GetAll(C_READ_ALL);
        }

        public Visit? GetByID(int id)
        {
            return GetByID(C_READ_BY_ID, id);
        }

        public void Save(Visit element)
        {
            Update(C_UPDATE, element, element.ID);
        }

        protected override Visit Map(SqlDataReader reader)
        {
            return new Visit(
                        Convert.ToInt32(reader[0].ToString()),
                        new UserDAOImpl().GetByID(Convert.ToInt32(reader[1].ToString())),
                        Convert.ToDateTime(reader[2].ToString())
                    );
        }

        protected override List<SqlParameter> Map(Visit obj)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@userId", obj.User.ID),
                new SqlParameter("@visit_date", obj.SqlTime)
            };
        }

        public List<Visit> GetUserVisits(int id)
        {
            var paramers = new List<SqlParameter>
            {
                new SqlParameter("@id", id),
            };
            return Get(C_READ_BY_USER_ID, paramers);
        }
    }
}
