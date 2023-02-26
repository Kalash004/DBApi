﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataBaseProject.DAOS;
using DataBaseProject.DBEntities;
using DataBaseProject.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace DataBaseProject.Manager
{
    internal class DBManager
    {
        HaircutDAOImpl haircutDAO = new HaircutDAOImpl();
        ItemsDAOImpl itemsDAO = new ItemsDAOImpl();
        PaidActionDAOImpl paidActionDAO = new PaidActionDAOImpl();
        PaintDAOImpl paintDAO = new PaintDAOImpl();
        StaffDAOImpl staffDAO = new StaffDAOImpl();
        UserDAOImpl userDAO = new UserDAOImpl();
        VisitDAOImpl visitDAO = new VisitDAOImpl();



        [Obsolete("Method is deprecated")]
        public void PrintAllData()
        {
            var hairs = haircutDAO.GetAll();
            var items = itemsDAO.GetAll();
            var paids = paidActionDAO.GetAll();
            var paints = paintDAO.GetAll();
            var staffs = staffDAO.GetAll();
            var users = userDAO.GetAll();
            var visits = visitDAO.GetAll();

            foreach (var item in hairs)
            {
                Console.WriteLine(item.ToString());
            }

            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }

            foreach (var item in paids)
            {
                Console.WriteLine(item.ToString());
            }

            foreach (var item in paints)
            {
                Console.WriteLine(item.ToString());
            }

            foreach (var item in staffs)
            {
                Console.WriteLine(item.ToString());
            }

            foreach (var item in users)
            {
                Console.WriteLine(item.ToString());
            }

            foreach (var item in visits)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void Menu()
        {
            Console.Clear();
            int chose = Chose("Please Choose Action: 1)Read, 2)Change, 3)Remove, 4)Add, 5)Export", 1, 5);
            switch (chose)
            {
                case 1:
                    Read();
                    break;
                case 2:
                    Update();
                    break;
                case 3:
                    Remove();
                    break;
                case 4:
                    Add();
                    break;
                case 5:
                    Export();
                    break;
            }
        }

        private void Export()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            int chose = Chose("Please Choose Which Table to Read: 1)Users, 2)Staff, 3)Visits, 4)Paints, 5)Haircuts", 1, 5);
            switch (chose)
            {
                case 1:
                    foreach (var str in ReadUsers())
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 2:
                    foreach (var str in ReadStaff())
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 3:
                    foreach (var str in ReadVisits())
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 4:
                    foreach (var str in ReadPaint())
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 5:
                    foreach (var str in ReadHaircuts())
                    {
                        Console.WriteLine(str);
                    }
                    break;
            }
            Console.WriteLine("To return to menu press any key");
            Console.ReadKey(true);
        }

        public void Add()
        {
            int chose = Chose("Please Choose to Which Table Add an Entity: 1)Users", 1, 1);
            switch (chose)
            {
                case 1:
                    AddUser();
                    break;
            }
        }
        public void Remove()
        {
            try
            {
                int chose = Chose("Please Choose Which Table to Remove from: 1)Users", 1, 1);
                switch (chose)
                {
                    case 1:
                        RemoveUser();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("You cant delete this user, because he has visited our establishment");
                Console.WriteLine("Press enter to return to menu");
                Console.ReadKey(true);
                Menu();
            }
        }
        public void Update()
        {
            int chose = Chose("Please Choose Which Table to Update: 1)Users", 1, 1);
            switch (chose)
            {
                case 1:
                    UpdateUser();
                    break;
                    //case 2:
                    //    foreach (var str in ReadStaff())
                    //    {
                    //        Console.WriteLine(str);
                    //    }
                    //    break;
                    //case 3:
                    //    foreach (var str in ReadVisits())
                    //    {
                    //        Console.WriteLine(str);
                    //    }
                    //    break;
                    //case 4:
                    //    foreach (var str in ReadPaint())
                    //    {
                    //        Console.WriteLine(str);
                    //    }
                    //    break;
                    //case 5:
                    //    foreach (var str in ReadHaircuts())
                    //    {
                    //        Console.WriteLine(str);
                    //    }
                    //    break;
            }
            Console.WriteLine("To return to menu press any key");
            Console.ReadKey(true);
        }

        public User AddUser()
        {
            String[] questions = { "Write User id", "Users Name", "Users Surname", "Total Money Spent" };
            User user = null;
            try
            {
                user = UserCreater(questions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Wasnt able to add/create user, please try again and check you are answering the right way");
                AddUser();
            }
            userDAO.Create(user);
            return user;
        }

        public void RemoveUser()
        {
            var user_strings = ReadUsers();
            foreach (var str in user_strings)
            {
                Console.WriteLine(str);
            }
            int chose = Chose("Please specify the id of the user you want to remove : ", 0, user_strings.Count());
            userDAO.Delete(chose);
        }

        public void UpdateUser()
        {
            var user_strings = ReadUsers();
            foreach (var str in user_strings)
            {
                Console.WriteLine(str);
            }
            int chose = Chose("Please specify the id of the user you want to change : ", 0, user_strings.Count());
            User usr = null;
            try
            {
                usr = userDAO.GetByID(chose);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Error: Most likely you didnt write id the right way, retry");
                UpdateUser();
            }
            chose = Chose("Which property to change ? 1)User id, 2)Name, 3)Surname, 4)Total money spent", 1, 4);
            switch (chose)
            {
                case 1:
                    try
                    {
                        int change = Chose("Input value: ", -1999999999, 1999999999);
                        usr.User_id = change;
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    break;
                case 2:
                    try
                    {
                        Console.WriteLine("Write Name: ");
                        usr.Name = Console.ReadLine();
                    }
                    catch (Exception e)
                    {

                    }
                    break;
                case 3:
                    try
                    {
                        Console.WriteLine("Write Surname: ");
                        usr.Surname = Console.ReadLine();
                    }
                    catch (Exception e)
                    {

                    }
                    break;
                case 4:
                    try
                    {
                        int change = Chose("Input value: ", -1999999999, 1999999999);
                        usr.Total_spent = change;
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    break;
            }
            userDAO.Save(usr);
        }
        private int Chose(String choses, int min, int max)
        {
            Console.WriteLine(choses);

            int x = -1;
            while (x == -1)
            {
                if (int.TryParse(Console.ReadLine(), out x)) //TODO: maybe tell user about max min
                {
                }
                else
                {
                    Console.WriteLine("You didnt write a number");
                    x = -1;
                }
            }
            return x;

        }

        private User UserCreater(String[] questions)
        {
            List<String> answers = new List<string>();
            foreach (var q in questions)
            {
                bool answered = false;
                while (!answered)
                {
                    Console.WriteLine(q);
                    var answer = Console.ReadLine();
                    Console.WriteLine(q + " : " + answer + " | Y/N ?");
                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        answers.Add(answer);
                        answered = true;
                    }
                }
            }
            return new User(int.Parse(answers[0]), answers[1], answers[2], int.Parse(answers[3]));
        }
        public IEnumerable<String> ReadUsers()
        {
            foreach (var user in userDAO.GetAll())
            {
                yield return user.ToString();
            }
        }

        public IEnumerable<String> ReadStaff()
        {
            foreach (var staff in staffDAO.GetAll())
            {
                yield return staff.ToString();
            }
        }

        public IEnumerable<String> ReadPaint()
        {
            foreach (var paint in paintDAO.GetAll())
            {
                yield return paint.ToString();
            }
        }

        public IEnumerable<String> ReadHaircuts()
        {
            foreach (var hair in haircutDAO.GetAll())
            {
                yield return hair.ToString();
            }
        }

        public IEnumerable<String> ReadVisits()
        {
            foreach (var visit in visitDAO.GetAll())
            {
                String retr = visit.ToString();
                var items = itemsDAO.GetAllByVisitID(visit.ID);
                foreach (var itm in items)
                {
                    retr = retr + " " + itm.ToString();
                }
                yield return retr;
            }
        }
    }
}
