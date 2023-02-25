using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataBaseProject.DBEntities;
using DataBaseProject.Interfaces;

namespace DataBaseProject.DAOS
{
    internal class PaintDAOImpl : AbstractDAO<Paint>, IDAO<Paint>
    {
        private static String table = "Paints";
        private String C_CREATE = String.Format("INSERT INTO {0} (p_name, remains, price) VALUES (@p_name, @remains, @price)", table);
        private String C_UPDATE = String.Format("UPDATE {0} SET p_name = @p_name, remains = @remains, price = @price, WHERE id = @id", table);
        private String C_READ_ALL = String.Format("SELECT * FROM {0}", table);
        private String C_READ_BY_ID = String.Format("SELECT * FROM {0} WHERE id = @id", table);
        private String C_DELETE = String.Format("DELETE FROM {0} WHERE id = @id", table);

        public int Create(Paint element)
        {
            return Create(C_CREATE, element);
        }

        public void Delete(int id)
        {
            Delete(C_DELETE, id);
        }

        public List<Paint> GetAll()
        {
            return GetAll(C_READ_ALL);
        }

        public Paint? GetByID(int id)
        {
            return GetByID(C_READ_BY_ID, id);
        }

        public void Save(Paint element)
        {
            Update(C_UPDATE, element, element.ID);
        }

        protected override Paint Map(SqlDataReader reader)
        {
            return new Paint(
                Convert.ToInt32(reader[0].ToString()),
                reader[1].ToString(),
                Convert.ToInt32(reader[2].ToString()),
                Convert.ToInt32(reader[3].ToString())
                );
        }

        protected override List<SqlParameter> Map(Paint obj)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@p_name",obj.P_name),
                new SqlParameter("@remains",obj.Remains),
                new SqlParameter("@price",obj.Price)
            };
        }
    }
}
