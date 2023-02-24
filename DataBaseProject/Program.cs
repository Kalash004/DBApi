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
            User user1 = userdao.GetByID(1);
            Console.WriteLine(user1.ToString());
            VisitDAO visitdao = new VisitDAO();
            Visit visit1 = new Visit(user1,new DateTime(2005,1,4,12,12,12));

            foreach (var visit in visitdao.GetAll())
            {
                Console.WriteLine(visit.ToString());
            }

        }
    }
}
