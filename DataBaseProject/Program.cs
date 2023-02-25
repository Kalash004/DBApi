using DataBaseProject.DAOS;
using DataBaseProject.DBEntities;
using DataBaseProject.Exporters;
using DataBaseProject.Interfaces;

namespace DataBaseProject
{

    internal class Program
    {
        static void Main(string[] args)
        {
          IDAO<User> userDao = new UserDAOImpl();
           Console.WriteLine(userDao.GetByID(1).ToString());

            IDataExporter exp = new JSONExporter();
            exp.Export("");
        }
    }
}
