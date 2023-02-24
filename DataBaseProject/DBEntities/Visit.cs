using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseProject.Interfaces;

namespace DataBaseProject.DBEntities
{
    internal class Visit : IBaseClass
    {
        private int id;
        private User user;
        private DateTime time;

        public Visit(User user, DateTime time)
        {
            this.id = -1;
            this.user = user;
            this.time = time;
        }

        public Visit(int id, User user, DateTime time)
        {
            this.id = id;
            this.user = user;
            this.time = time;
        }

        public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime Time { get => time; set => time = value; }
        internal User User { get => user; set => user = value; }
    }
}
