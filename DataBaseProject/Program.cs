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
            VisitDAOImpl visitdao = new VisitDAOImpl();
            foreach (var visit in visitdao.GetAll())
            {
                Console.WriteLine(visit.ToString());
            }
            
        }
    }
}
