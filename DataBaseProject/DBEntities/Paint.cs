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
        private string remains;
        private int price;

        public Paint(string p_name, string remains, int price)
        {
            this.id = -1;
            this.p_name = p_name;
            this.remains = remains;
            this.price = price;
        }

        public Paint(int id, string p_name, string remains, int price)
        {
            this.id = id;
            this.p_name = p_name;
            this.remains = remains;
            this.price = price;
        }

        public int ID { get => id; set => id = value; }
        public string P_name { get => p_name; set => p_name = value; }
        public string Remains { get => remains; set => remains = value; }
        public int Price { get => price; set => price = value; }
    }
}
