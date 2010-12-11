// Idbo
// Idbo sample application
//
// Copyright © Ilyas Kolasinac Osmanogullari, 2010
// ilyax.os@hotmail.com || http://www.ilyax.com
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idboEntity;

namespace idboDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // OPEN CONNECTION
            ConnectIdbo();

            CreateMenu();
        }

        /// <summary>
        /// Menu
        /// </summary>
        private static void CreateMenu()
        {
            Console.WriteLine("------------------Welcome Message-------------------");
            Console.WriteLine("1-) Insert Car");
            Console.WriteLine("2-) Insert Friend");
            Console.WriteLine("");
            Console.WriteLine("---------------------List---------------------------");
            Console.WriteLine("4-) Car List");
            Console.WriteLine("5-) Friend List");

            Console.WriteLine("");
            Console.WriteLine("7-) Exit");

            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    InsertCar();
                    break;
                case 2:
                    InsertFriend();
                    break;
                case 4:
                    ListCar();
                    break;
                case 5:
                    ListFriend();
                    break;
                case 7:
                    Idbo.IdboConnection.CloseDB();// optional
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }

        }

        #region Insert

        /// <summary>
        /// * Create new instance for entity  "Car"
        /// * run Idbo Helper insert function 
        /// ps: Idbo helper need your entity
        /// </summary>
        private static void InsertCar()
        {
            Console.WriteLine("");
            Console.WriteLine("Insert Car");
            Console.WriteLine("\r\n");

            Car tmpCar = new Car();
            tmpCar.ID = Idbo.IdboTools.CreateGUI();

            Console.Write("Car Year :");
            tmpCar.Year = Convert.ToInt32(Console.ReadLine());

            Console.Write("Car Model :");
            tmpCar.Model = Console.ReadLine();

            Console.WriteLine(tmpCar.Year + " " + tmpCar.Model);

            Idbo.IdboHelper<Car>.Insert(tmpCar);

            CreateMenu();
        }

        /// <summary>
        /// * Create new instance for entity  "Friend"
        /// * run Idbo Helper insert function 
        /// ps: Idbo helper need your entity
        /// </summary>
        private static void InsertFriend()
        {
            Console.WriteLine("");
            Console.WriteLine("Insert Friend");
            Console.WriteLine("\r\n");

            Friend tmpFriend = new Friend();
            tmpFriend.ID = Idbo.IdboTools.CreateGUI();

            Console.Write("Name :");
            tmpFriend.Name = Console.ReadLine();

            Console.Write("Surname :");
            tmpFriend.Surname = Console.ReadLine();

            Console.Write("BirthDay :");
            tmpFriend.BirthDay = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine(tmpFriend.Name + " " + tmpFriend.Surname + " " + tmpFriend.BirthDay.ToShortDateString());

            Idbo.IdboHelper<Friend>.Insert(tmpFriend);

            CreateMenu();
        }

        #endregion

        #region List

        /// <summary>
        /// Idbo only need your entity
        /// Idbo.IdboHelper<Entity>.SelectAll()
        /// </summary>
        private static void ListCar()
        {
            List<Car> carList = Idbo.IdboHelper<Car>.SelectAll();
            foreach (Car tmpCar in carList)
            {
                Console.WriteLine("      " + tmpCar.Model + "    " + tmpCar.Year.ToString());
            }

            Console.ReadKey(false);
            CreateMenu();
        }

        /// <summary>
        /// Idbo only need your entity
        /// Idbo.IdboHelper<Entity>.SelectAll()
        /// Order by descending Birthday Year
        /// </summary>
        private static void ListFriend()
        {
            IEnumerable<Friend> friendList = Idbo.IdboHelper<Friend>.SelectAll().OrderByDescending(desc=>desc.BirthDay.Year);;

            foreach (Friend tmpFriend in friendList)
            {
                Console.WriteLine("      " + tmpFriend.Name + " " + tmpFriend.Surname + " " + tmpFriend.BirthDay.ToShortDateString());
            }

            Console.ReadKey(false);
            CreateMenu();
        }

        #endregion

        /// <summary>
        /// Connection Open
        /// </summary>
        private static void ConnectIdbo()
        {
            try
            {
                Idbo.IdboConnection.SetDb();
                Console.WriteLine("connection success");
                Console.WriteLine("\r\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("connection problem");
                Console.WriteLine(ex.ToString());
            }
        }

    }// class end
}
