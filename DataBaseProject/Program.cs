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
            bool done = false;
            var manager = new DBManager();
            while (!done)
            {
                manager.Menu();
            }
         
        }
    }
}
