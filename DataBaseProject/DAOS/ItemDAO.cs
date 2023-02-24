using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseProject.DBEntities;
using DataBaseProject.Interfaces;

namespace DataBaseProject.DAOS
{
    internal class ItemDAO : IDAO<Item>
    {
        public void Delete(Item element)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetAll()
        {
            throw new NotImplementedException();
        }

        public Item? GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Item element)
        {
            throw new NotImplementedException();
        }
    }
}
