using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseProject.Interfaces;

namespace DataBaseProject.DBEntities
{
    internal class Paint : IBaseClass
    {
        private int id;
        private string p_name;
        private int remains;
        private int price;

        public Paint(string p_name, int remains, int price)
        {
            this.id = -1;
            this.p_name = p_name;
            this.remains = remains;
            this.price = price;
        }

        public Paint() { }
        public Paint(int id, string p_name, int remains, int price)
        {
            this.id = id;
            this.p_name = p_name;
            this.remains = remains;
            this.price = price;
        }

        public int ID { get => id; set => id = value; }
        public string P_name { get => p_name; set => p_name = value; }
        public int Remains { get => remains; set => remains = value; }
        public int Price { get => price; set => price = value; }
    }
}
