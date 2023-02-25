using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseProject.Interfaces;

namespace DataBaseProject.DBEntities
{
    /// <summary>
    /// Polozka
    /// </summary>
    internal class Item:IBaseClass
    {
        private int id;
        private Visit visit;
        private Staff staff_on_duty;
        private PaidAction action;

        public Item(Visit visit, Staff staff_on_duty, PaidAction action)
        {
            this.id = -1;
            this.visit = visit;
            this.staff_on_duty = staff_on_duty;
            this.action = action;
        }
        public Item() { }
        public Item(int id, Visit visit, Staff staff_on_duty, PaidAction action)
        {
            this.id = id;
            this.visit = visit;
            this.staff_on_duty = staff_on_duty;
            this.action = action;
        }

        public int ID { get => id; set => id = value; }
        internal Visit Visit { get => visit; set => visit = value; }
        internal Staff Staff_on_duty { get => staff_on_duty; set => staff_on_duty = value; }
        internal PaidAction Action { get => action; set => action = value; }
    }
}
