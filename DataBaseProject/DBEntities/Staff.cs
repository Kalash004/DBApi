using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseProject.Interfaces;

namespace DataBaseProject.DBEntities
{
    internal class Staff:IBaseClass
    {
        private int id;
        private int staff_id;
        private string name;
        private string surname;
        private int month_pay;

        public Staff(int id, int staff_id, string name, string surname, int month_pay)
        {
            this.id = id;
            this.staff_id = staff_id;
            this.name = name;
            this.surname = surname;
            this.month_pay = month_pay;
        }

        public Staff() { }
        public Staff(int staff_id, string name, string surname, int month_pay)
        {
            this.id = -1;
            this.staff_id = staff_id;
            this.name = name;
            this.surname = surname;
            this.month_pay = month_pay;
        }

        public int ID { get => id; set => id = value; }
        public int Staff_id { get => staff_id; set => staff_id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public int Month_pay { get => month_pay; set => month_pay = value; }

        public override string ToString()
        {
            return String.Format("Staff - Id: {0},Staff id :{4} Name: {1} {2}, Pay: {3}",ID,Name,Surname,Month_pay,Staff_id);
        }
    }
}
