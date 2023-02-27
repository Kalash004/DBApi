using System.ComponentModel.Design;
using DataBaseProject.DAOS;
using DataBaseProject.DBEntities;
using DataBaseProject.Exporters;
using DataBaseProject.Interfaces;
using DataBaseProject.Manager;

namespace DataBaseProject
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var manager = new DBManager();
            while (true)
            {
                manager.Menu();
            }
         
        }
    }
}
