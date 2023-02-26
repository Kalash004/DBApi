using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseProject.Interfaces;

namespace DataBaseProject.DBEntities
{
    internal class Haircut : IBaseClass
    {
        private int id;
        private string name;
        private string description;
        private int price;

        public Haircut(string name, string description, int price)
        {
            this.id = -1;
            this.name = name;
            this.description = description;
            this.price = price;
        }

        public Haircut() { }
        public Haircut(int id, string name, string description, int price)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.price = price;
        }

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int Price { get => price; set => price = value; }

        public override string ToString()
        {
            return String.Format("Haircut - Id: {0}, Name: {1}, Price: {2}",ID,Name,Price);
        }
    }
}
