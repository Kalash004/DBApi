using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public int ID { get => id; set => this.id = value; }
        public DateTime Time { get => time; set => time = value; }

        public String SqlTime {
            get { 
                String sqlFormattedDate = this.time.ToString("yyyy-MM-dd HH:mm:ss.fff");
                return sqlFormattedDate;
            }
        }
        internal User User { get => user; set => user = value; }
    }
}
