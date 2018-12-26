using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Homework_Assessment
{
    class Program
    {
        struct homework // contains all variables for homework save file
        {
            public string subject;
            public string description;
            public DateTime duedate;
            public bool completed;
        }
        const int maxhw = 20; // max amount of homeworks allowed
        const int hwcountt = 0;
        static void menu()
        {
            Console.Clear();
            Console.WriteLine("1) Enter Hw");
            Console.WriteLine("2) Update Hw");
            Console.WriteLine("3) View Hw");
            Console.WriteLine("4) Exit");
        }
        static void Main(string[] args)
        {
            homework[] array = new homework[20]; // homework array
            string choice = "";
            while (choice != "5")
            {
                menu();
                Console.WriteLine("What do you want to do?:");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        enterhw(array);
                        break;
                    case "2":
                        update(array);
                        break;
                    case "3":
                        Load(array);
                        break;
                    case "4":
                        System.Environment.Exit(0); //will exit the file if 4 is inputted
                        break;
                    default: 
                        Console.WriteLine("Invalid selection"); // will make sure code does not break depending on user input
                        break;
                }
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }
        }
        static void Load(homework[] array)
        {
            StreamReader hw = new StreamReader("Homework.txt"); //reads the homework text file
            int hwcount = 0;
            while (hw.EndOfStream == false)
            {
                homework loaded;
                loaded.subject = hw.ReadLine();
                loaded.description = hw.ReadLine();
             loaded.duedate = Convert.ToDateTime(hw.ReadLine());
           loaded.completed= Convert.ToBoolean(hw.ReadLine());
                hwcount += 1;
                hwcount = hwcountt;
            }
            hw.Close();
            Console.WriteLine("Homework Loaded");
            view(array, hwcountt);
            Console.ReadKey();
        }
        static void save(homework[] array)
        {
            StreamWriter File = new StreamWriter("Homework.txt", false); // writes into file and overwrites completed as false
            for (int i = 0; i < 1; i++)
            {
                string y = array[i].subject;
                File.WriteLine("Subject: {0} || ", y);
                string x = array[i].description;
                File.WriteLine("description: {0} || ", x);
                DateTime z = array[i].duedate;
                File.WriteLine("duedate: {0} || ", z);
                bool a = array[i].completed;
                File.WriteLine("completed: {0} ", a);
                File.WriteLine("\n"); //leaves a space inbetween saved files    
            }
            File.Close();
            Console.WriteLine("File saved");
            Console.ReadKey();
        }
        static void update(homework[] array)
        { int position = 0;
            bool find = false;
            string hwtofind;
            Console.WriteLine("Enter HW to update: ");
            hwtofind = Console.ReadLine();
            hwtofind = hwtofind.ToUpper();
            while (position <= hwcountt & find == false)
            {
                if (array[position].subject == hwtofind)
                {
                    find = true;
                }
                else
                { position = position + 1;
                }

                if (find == true)
                {
                    array[position].completed = true;
                    Console.WriteLine("Homework Updated");
                }
                else
                {
                    Console.WriteLine("Homework not found");
                }
                Console.ReadKey();
            }

        }
        static void view (homework[] array, int hwcountt)
        {
            DateTime date = DateTime.Now;
            TimeSpan deadline = array[hwcountt].duedate - date;
            int dure = Convert.ToInt32(deadline.Days);
            bool valid = true;
            if (dure >= 3)
            {
                valid = true;
            }
            else
            { valid = false; }
            for (int i = 1; i <= hwcountt; i++)
            {
                if (array[i].completed == false & valid == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // outputs whether the homework is completed or not
                }
                else if (array[i].completed == true & valid == true) // outputs green if the homework is completed
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine(array[i].subject);
                Console.WriteLine(array[i].description);
                Console.WriteLine(array[i].duedate);
                Console.WriteLine("");
            }
        }

        static void enterhw(homework[] array)
        {
            bool valid = false;
            do
                for (int i = 0; i < 1; i++)
                {
                    Console.WriteLine("What is the subject?");
                    string subject = Console.ReadLine();
                    array[i].subject = subject;
                    Console.WriteLine("Write a description");
                    string description = Console.ReadLine();
                    array[i].description = description;
                    Console.WriteLine("When is the hw due?");
                    try
                    {
                        DateTime date = DateTime.Parse(Console.ReadLine());
                        array[i].duedate = date;
                        valid = true;
                    }
                    catch (Exception) // catches invalid inputs that may have been inputted
                    {
                        Console.WriteLine("not a date (must be DD/MM/YYYY)");
                        Console.ReadKey();
                        menu();
                    }
                }
            while (valid == false);
            save(array);
        }
    }
}

