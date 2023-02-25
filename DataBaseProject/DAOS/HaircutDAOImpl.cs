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
    internal class HaircutDAOImpl : AbstractDAO<Haircut>, IDAO<Haircut>
    {
        private static String table = "Haircuts";
        private String C_CREATE = String.Format("INSERT INTO {0} (h_name, h_description, price) VALUES (@h_name, @h_description, @price)", table);
        private String C_UPDATE = String.Format("UPDATE {0} SET h_name = @h_name, h_description = @h_description, price = @price, WHERE id = @id", table);
        private String C_READ_ALL = String.Format("SELECT * FROM {0}", table);
        private String C_READ_BY_ID = String.Format("SELECT * FROM {0} WHERE id = @id", table);
        private String C_DELETE = String.Format("DELETE FROM {0} WHERE id = @id", table);


        public int Create(Haircut element)
        {
            return Create(C_CREATE, element);
        }

        public void Delete(int id)
        {
            Delete(C_DELETE, id);
        }

        public List<Haircut> GetAll()
        {
            return GetAll(C_READ_ALL);
        }

        public Haircut? GetByID(int id)
        {
            return GetByID(C_READ_BY_ID, id);
        }

        public void Save(Haircut element)
        {
            Update(C_CREATE, element, element.ID);
        }

        protected override Haircut Map(SqlDataReader reader)
        {
            return new Haircut(
                   Convert.ToInt32(reader[0].ToString()),
                   reader[1].ToString(),
                   reader[2].ToString(),
                   Convert.ToInt32(reader[3].ToString())
                   );
        }
         
        protected override List<SqlParameter> Map(Haircut obj)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@h_name", obj.Name),
                new SqlParameter("@h_description", obj.Description),
                new SqlParameter("@price", obj.Price)
            };
        }
    }
}
