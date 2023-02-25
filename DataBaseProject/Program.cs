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
            HaircutDAOImpl hairdao = new HaircutDAOImpl();
            StaffDAOImpl staffdao = new StaffDAOImpl();
            PaintDAOImpl paintdao = new PaintDAOImpl();
            VisitDAOImpl visitdao = new VisitDAOImpl();  
            PaidActionDAOImpl paidActionDAOImpl = new PaidActionDAOImpl();
            ItemsDAOImpl itemsDAO = new ItemsDAOImpl();
            itemsDAO.Create(new Item(visitdao.GetByID(1),staffdao.GetByID(2),paidActionDAOImpl.GetByID(1)));
        }
    }
}
