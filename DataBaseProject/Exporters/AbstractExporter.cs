using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseProject.DAOS;
using DataBaseProject.DBEntities;
using DataBaseProject.Interfaces;

namespace DataBaseProject.Exporters
{
    internal abstract class AbstractExporter
    {
        protected List<User> GetData()
        {
            IDAO<User> userDao = new UserDAOImpl();
            return userDao.GetAll();
        }

    }
}
