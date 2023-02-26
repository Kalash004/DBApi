using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseProject.Interfaces;

namespace DataBaseProject.DBEntities
{
    public class User : IBaseClass
    {
            
        private int id;
        private int user_id;
        private string name;
        private string surname;
        private int total_spent;
        private List<Visit> visits;

        public User(int id, int user_id, string name, string surname, int total_spent)
        {
            this.id = id;
            this.user_id = user_id;
            this.name = name;
            this.surname = surname;
            this.total_spent = total_spent;
        }
        public User()
        {
        }
        public User(int user_id, string name, string surname, int total_spent)
        {
            this.id = -1;
            this.user_id = user_id;
            this.name = name;
            this.surname = surname;
            this.total_spent = total_spent;
        }
        public int ID { get => id; set => id = value; }
        public int User_id { get => user_id; set => user_id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public int Total_spent { get => total_spent; set => total_spent = value; }
        public List<Visit> Visits { get => visits; set => visits = value; }


        public void AddVisit(Visit visit)
        {
            if (this.Visits == null)
            {
                this.Visits = new List<Visit>();
            }
            this.Visits.Add(visit);
        }

        public override string ToString()
        {
            return String.Format("User - Id: {0},user id: {1},name: {2},surname: {3},total spent: {4}",ID,User_id,Name,Surname,Total_spent);
        }
    }
}
