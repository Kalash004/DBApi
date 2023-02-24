using DataBaseProject.DAOS;
using DataBaseProject.DBEntities;

namespace DataBaseProject
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("started");
            UserDAO userdao = new UserDAO();

            Console.WriteLine(userdao.GetByID(1).ToString());
        }
    }
}
