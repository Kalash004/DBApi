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
    internal class ItemsDAOImpl : AbstractDAO<Item>, IDAO<Item>
    {
        private static String table = "Items";
        private String C_CREATE = String.Format("INSERT INTO {0} (visitId, staffId, actionId) VALUES (@visitId, @staffId, @actionId)", table);
        private String C_UPDATE = String.Format("UPDATE {0} SET visitId = @visitId, staffId = @staffId, actionId = @actionId, WHERE id = @id", table);
        private String C_READ_ALL = String.Format("SELECT * FROM {0}", table);
        private String C_READ_BY_ID = String.Format("SELECT * FROM {0} WHERE id = @id", table);
        private String C_DELETE = String.Format("DELETE FROM {0} WHERE id = @id", table);
        private String C_GET_BY_VISIT_ID = String.Format("SELECT * FROM {0} WHERE visitId = @visitId",table);

        public int Create(Item element)
        {
            return Create(C_CREATE,element);
        }

        public void Delete(int id)
        {
            Delete(id);
        }

        public List<Item> GetAll()
        {
            return GetAll(C_READ_ALL);
        }

        public Item? GetByID(int id)
        {
            return GetByID(C_READ_BY_ID, id);
        }

        public void Save(Item element)
        {
            Update(C_UPDATE, element, element.ID);
        }

        public List<Item> GetAllByVisitID(int id)
        {
            return GetByConnectingID(C_GET_BY_VISIT_ID,id,"@visitId");
        }

        protected override Item Map(SqlDataReader reader)
        {
            return new Item(
                Convert.ToInt32(reader[0].ToString()),
                new VisitDAOImpl().GetByID(Convert.ToInt32(reader[1].ToString())),
                new StaffDAOImpl().GetByID(Convert.ToInt32(reader[2].ToString())),
                new PaidActionDAOImpl().GetByID(Convert.ToInt32(reader[3].ToString()))
                );
        }

        protected override List<SqlParameter> Map(Item obj)
        {
            //visitId, staffId, actionId
            return new List<SqlParameter>
            {
                new SqlParameter("@visitId",obj.Visit.ID),
                new SqlParameter("@staffId",obj.Staff_on_duty.ID),
                new SqlParameter("@actionId",obj.Action.ID)
            };
        }
    }
}
