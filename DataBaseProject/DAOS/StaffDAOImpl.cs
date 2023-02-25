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
    internal class StaffDAOImpl : AbstractDAO<Staff>, IDAO<Staff>
    {
        private static String table = "Staff";
        private String C_CREATE = String.Format("INSERT INTO {0} (staff_id, month_pay, s_name, s_surname) VALUES (@staff_id, @month_pay, @s_name, @s_surname)", table);
        private String C_UPDATE = String.Format("UPDATE {0} SET staff_id = @staff_id, month_pay = @month_pay, s_name = @s_name, s_surname = @s_surname WHERE id = @id", table);
        private String C_READ_ALL = String.Format("SELECT * FROM {0}", table);
        private String C_READ_BY_ID = String.Format("SELECT * FROM {0} WHERE id = @id", table);
        private String C_DELETE = String.Format("DELETE FROM {0} WHERE id = @id", table);

        public int Create(Staff element)
        {
            return Create(C_CREATE, element);
        }

        public void Delete(int id)
        {
            Delete(C_DELETE, id);
        }

        public List<Staff> GetAll()
        {
            return GetAll(C_READ_ALL);
        }

        public Staff? GetByID(int id)
        {
            return GetByID(C_READ_BY_ID, id);
        }

        public void Save(Staff element)
        {
            Update(C_UPDATE, element, element.ID);
        }

        protected override Staff Map(SqlDataReader reader)
        {
            return new Staff(
               Convert.ToInt32(reader[0].ToString()),
               Convert.ToInt32(reader[1].ToString()),
               reader[3].ToString(),
               reader[4].ToString(),
               Convert.ToInt32(reader[2].ToString())
             );
        }

        protected override List<SqlParameter> Map(Staff obj)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@staff_id",obj.Staff_id),
                new SqlParameter("@month_pay",obj.Month_pay),
                new SqlParameter("@s_name",obj.Name),
                new SqlParameter("@s_surname",obj.Surname),
            };
        }
    }
}
