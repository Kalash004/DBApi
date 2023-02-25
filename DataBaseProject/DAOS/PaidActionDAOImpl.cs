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
    internal class PaidActionDAOImpl : AbstractDAO<PaidAction>, IDAO<PaidAction>
    {
        private static String table = "PaidAction";
        private String C_CREATE = String.Format("INSERT INTO {0} (haircutId, paintId) VALUES (@haircutId, @paintId)", table);
        private String C_UPDATE = String.Format("UPDATE {0} SET haircutId = @haircutId, paintId = @paintId WHERE id = @id", table);
        private String C_READ_ALL = String.Format("SELECT * FROM {0}", table);
        private String C_READ_BY_ID = String.Format("SELECT * FROM {0} WHERE id = @id", table);
        private String C_DELETE = String.Format("DELETE FROM {0} WHERE id = @id", table);

        public int Create(PaidAction element)
        {
            return Create(C_CREATE,element);
        }

        public void Delete(int id)
        {
            Delete(id);
        }

        public List<PaidAction> GetAll()
        {
            return GetAll(C_READ_ALL);
        }

        public PaidAction? GetByID(int id)
        {
            return GetByID(C_READ_BY_ID,id);
        }

        public void Save(PaidAction element)
        {
            Update(C_UPDATE, element, element.ID);
        }

        protected override PaidAction Map(SqlDataReader reader)
        {
            int id = Convert.ToInt32(reader[0].ToString());
            Haircut hair = new HaircutDAOImpl().GetByID(Convert.ToInt32(reader[1].ToString()));
            Paint paint = new PaintDAOImpl().GetByID(Convert.ToInt32(reader[2].ToString()));

            return new PaidAction(
                id,
                hair,
                paint
                ); 
        }

        protected override List<SqlParameter> Map(PaidAction obj)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@haircutId",obj.Haircut.ID),
                new SqlParameter("@paintId",obj.Paint.ID)
            };
        }
    }
}
