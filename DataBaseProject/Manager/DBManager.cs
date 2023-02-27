using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DataBaseProject.DAOS;
using DataBaseProject.DBEntities;
using DataBaseProject.Exporters;
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
        CSVImporter importer = new CSVImporter();



        [Obsolete("Method is deprecated")]
        private void PrintAllData()
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
            int chose = Choose("Please Choose Action: 1)Read, 2)Change, 3)Remove, 4)Add, 5)Import");
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
                    Import();
                    break;
                default:
                    Console.WriteLine("Out of menu number");
                    break;
            }
        }

        private void Import()
        {
            Console.Clear();
            int chose = Choose("Please Choose Which Table to Import: 1)Users, 2)Paints, 3)Haicuts");
            switch (chose)
            {
                case 1:
                    ImportUsers();
                    break;
                case 2:
                    ImportPaints();
                    break;
                case 3:
                    ImportHaircuts();
                    break;
                default:
                    Console.WriteLine("Out of menu number");
                    break;
            }
        }

        private void ImportHaircuts()
        {
            Console.WriteLine("Please write pathfile of the .csv table (example :"+ @"\C:\programs\file.txt\)");
            String path = Console.ReadLine();
            importer.ImportHaircuts(path);
        }

        private void ImportPaints()
        {
            Console.WriteLine("Please write pathfile of the .csv table (example :" + @"\C:\programs\file.txt\)");
            String path = Console.ReadLine();
            importer.ImportPaint(path);
        }

        private void ImportUsers()
        {
            Console.WriteLine("Please write pathfile of the .csv table (example :" + @"\C:\programs\file.txt\)");
            String path = Console.ReadLine();
            importer.ImportUser(path);
        }

        private void Export()
        {
            throw new NotImplementedException();
        }
        public void Read()
        {
            int chose = Choose("Please Choose Which Table to Read: 1)Users, 2)Staff, 3)Visits, 4)Paints, 5)Haircuts");
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
                default:
                    Console.WriteLine("Out of menu number");
                    break;
            }
            Console.WriteLine("To return to menu press any key");
            Console.ReadKey(true);
        }
        public void Add()
        {
            int chose = Choose("Please Choose to Which Table Add an Entity: 1)Users 2)Visit 3)Haircut 4)Paint 5)Staff");
            switch (chose)
            {
                case 1:
                    AddUser();
                    break;
                case 2:
                    AddVisit();
                    break;
                case 3:
                    AddHairCut();
                    break;
                case 4:
                    AddPaint();
                    break;
                case 5:
                    AddStaff();
                    break;
                default:
                    Console.WriteLine("Out of menu number");
                    break;
            }
        }

        private void AddStaff()
        {
            String[] questions = {"Write staff id","Name","Surname","Payment"};
            Staff staff = StaffCreater(questions);
            try
            {
                new StaffDAOImpl().Create(staff);
            }
            catch
            {
                throw;
            }
        }

        private void AddPaint()
        {
            String[] questions = { "Write paint color", "How much of the paint remains in storage", "Price (only numbers)" };
            Paint paint = PaintCreater(questions);
            try
            {
                new PaintDAOImpl().Create(paint);
            }
            catch
            {
                throw;
            }
        }
        private void AddHairCut()
        {
            String[] questions = { "Write Haircut name", "Haircut description", "Price (only numbers)" };
            Haircut haircut = HaircutCreater(questions);    
            try
            {
                new HaircutDAOImpl().Create(haircut);
            } catch
            {
                throw;
            }
        }
        public void Remove()
        {
            try
            {
                int chose = Choose("Please Choose Which Table to Remove from: 1)Users");
                switch (chose)
                {
                    case 1:
                        RemoveUser();
                        break;
                    default:
                        Console.WriteLine("Out of menu number");
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
            int chose = Choose("Please Choose Which Table to Update: 1)Users");
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
                default:
                    Console.WriteLine("Out of menu number");
                    break;
            }
            Console.WriteLine("To return to menu press any key");
            Console.ReadKey(true);
        }

        //------------------------------------
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
            int chose = Choose("Please specify the id of the user you want to remove : ");
            userDAO.Delete(chose);
        }
        public void UpdateUser()
        {
            var user_strings = ReadUsers();
            foreach (var str in user_strings)
            {
                Console.WriteLine(str);
            }
            int chose = Choose("Please specify the id of the user you want to change : ");
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
            chose = Choose("Which property to change ? 1)User id, 2)Name, 3)Surname, 4)Total money spent");
            switch (chose)
            {
                case 1:
                    try
                    {
                        int change = Choose("Input value: ");
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
                        int change = Choose("Input value: ");
                        usr.Total_spent = change;
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    break;
                default:
                    Console.WriteLine("Out of menu number");
                    break;
            }
            userDAO.Save(usr);
        }
        // -----------------------------------
        public void AddVisit()
        {
            Visit visit = CreateVisit();
            CreateItems(visit);
        }
        

        private void CreateItems(Visit visit)
        {
            bool done = false;
            while (!done)
            {
                PaidAction paidAction = CreatePaidAction();
                Staff staff;
                foreach (var s in new StaffDAOImpl().GetAll())
                {
                    Console.WriteLine(s.ToString());
                }
                int choose = Choose("Please choose id of staff who was working");
                try
                {
                    staff = new StaffDAOImpl().GetByID(choose);
                } catch (Exception e)
                {
                    throw;
                }
                Item item = new Item(visit, staff, paidAction);
                new ItemsDAOImpl().Create(item);
            }
        }

        private PaidAction CreatePaidAction()
        {
            // choose paints and haircuts
            var haircuts = haircutDAO.GetAll();
            var paints = paintDAO.GetAll();
            foreach (var cuts in haircuts)
            {
                Console.WriteLine(cuts.ToString());
            }
            foreach (var paintt in paints)
            {
                Console.WriteLine(paintt.ToString());
            }
            int cutid = Choose("Please write the id of the haircut");
            int paintid = Choose("Please write the id of the paint");
            var cut = haircutDAO.GetByID(cutid);
            var paint = paintDAO.GetByID(paintid);
            PaidAction action = null;
            int id = 0;
            try
            {
                action = new PaidAction(cut, paint);
                id = paidActionDAO.Create(action);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("There was an error press enter to return back to menu");
                Console.ReadKey(true);
                Menu();
            }
            action.ID = id;
            return action;
        }

        private Visit CreateVisit()
        {
            foreach (var user_get in userDAO.GetAll())
            {
                Console.WriteLine(user_get.ToString());
            }
            int id = Choose("Please choose id of the user that visited");
            User user = userDAO.GetByID(id);
            DateTime time = CreateTime();
            Visit visit = new Visit(user,time);
            int another_id = -1;
            try
            {
                another_id = visitDAO.Create(visit);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("There was an error with creating visit");
            }
            visit.ID = another_id;
            return visit;
        }

        private DateTime CreateTime()
        {
            DateTime dateTime;
            try
            {
                Console.Write("Enter a month: ");
                int month = int.Parse(Console.ReadLine());
                Console.Write("Enter a day: ");
                int day = int.Parse(Console.ReadLine());
                Console.Write("Enter a year: ");
                int year = int.Parse(Console.ReadLine());
                Console.Write("Enter a hour: ");
                int hour = int.Parse(Console.ReadLine());
                Console.Write("Enter a minute: ");
                int minute = int.Parse(Console.ReadLine());
                Console.Write("Enter a second: ");
                int second = int.Parse(Console.ReadLine());
                dateTime = new DateTime(year, month, day, hour, minute, second);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("There was an error with creating date please retry");
                dateTime = CreateTime();
            }
                return dateTime;
        }
        public Haircut HaircutCreater(String[] questions)
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
            return new Haircut(answers[0], answers[1], int.Parse(answers[2]));
        }

        public Paint PaintCreater(String[] questions)
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
            return new Paint(answers[0], int.Parse(answers[1]), int.Parse(answers[2]));
        }

        public Staff StaffCreater(String[] questions)
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
            return new Staff(int.Parse(answers[0]), answers[1], answers[2], int.Parse(answers[3]));
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
        private int Choose(String choses)
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
    }
}
