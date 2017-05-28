using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BankAccountPrototype
{
    class Program
    {
        static int Main()
        {
            Console.WriteLine("\n     Federal Bank");
            Console.WriteLine("+--------------------+");
            Console.WriteLine("|     MAIN MENU      |");
            Console.WriteLine("+--------------------+");
            Console.WriteLine("|                    |");
            Console.WriteLine("|1.) Admin           |");
            Console.WriteLine("|2.) Customer        |");
            Console.WriteLine("|3.) Exit            |");
            Console.WriteLine("|                    |");
            Console.WriteLine("+--------------------+");

            Console.Write("\nEnter selection: ");
            int sel = int.Parse(Console.ReadLine());

            switch (sel)
            {
                case 1: AdminMenu(); break;
                case 2: CustomerMenu(); break;
                case 3: return 0;
                default:
                    Console.WriteLine("Invalid input, press enter to try again");
                    Console.ReadLine();
                    Main();
                    break;
            }

            return 0;
        }

        private static int AdminMenu()
        {
            Console.Clear();
            Console.WriteLine("\n     Federal Bank");
            Console.WriteLine("+--------------------+");
            Console.WriteLine("|     ADMIN MENU     |");
            Console.WriteLine("+--------------------+");
            Console.WriteLine("|                    |");
            Console.WriteLine("|1.) Login           |");
            Console.WriteLine("|2.) Back            |");
            Console.WriteLine("|3.) Exit            |");
            Console.WriteLine("|                    |");
            Console.WriteLine("+--------------------+");
            Console.Write("\nEnter Selection: ");
            int sel = int.Parse(Console.ReadLine());

            switch (sel)
            {
                case 1:
                    // AdminLogin()
                    break;
                case 2:
                    Console.Clear();
                    Main();
                    break;
                case 3:
                    return 0;
                default:
                    Console.WriteLine("Invalid input, press enter to try again");
                    Console.ReadLine();
                    AdminMenu();
                    break;
            }

            return 0;
        }

        private static int CustomerMenu()
        {
            Console.Clear();
            Console.WriteLine("\n     Federal Bank");
            Console.WriteLine("+--------------------+");
            Console.WriteLine("|   CUSTOMER MENU    |");
            Console.WriteLine("+--------------------+");
            Console.WriteLine("|                    |");
            Console.WriteLine("|1.) Login           |");
            Console.WriteLine("|2.) Create Account  |");
            Console.WriteLine("|3.) Back            |");
            Console.WriteLine("|4.) Exit            |");
            Console.WriteLine("|                    |");
            Console.WriteLine("+--------------------+");
            Console.Write("\nEnter Selection: ");
            int sel = int.Parse(Console.ReadLine());

            switch (sel)
            {
                case 1:
                    CustomerLogin(); 
                    break;
                case 2:
                    CreateCustomer();
                    break;
                case 3:
                    Console.Clear();
                    Main(); 
                    break;
                case 4:
                    return 0;
                default:
                    Console.WriteLine("Invalid input, press enter to try again");
                    Console.ReadLine();
                    CustomerMenu();
                    break;
            }

            return 0;
        }

        public static void CreateCustomer()
        {
            using (var db = new CustomerContext())
            {
                // Create and save a new Customer Account
                Console.Clear();
                Console.WriteLine("\n        Federal Bank");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|   NEW CUSTOMER ACCOUNT   |");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|                          |");

                Console.Write("|New Username: ");
                var username = Console.ReadLine();

                Console.Write("|New Password: ");
                Console.ForegroundColor = ConsoleColor.Black;
                var password = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write("|Re-enter Password: ");
                Console.ForegroundColor = ConsoleColor.Black;
                var samePass = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;

                while (password != samePass)
                {
                    Console.WriteLine("Passwords do not match, try again.");
                    Console.Write("|Re-enter Password:   |");
                    samePass = Console.ReadLine();
                }
                Console.WriteLine("|                          |");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("\n\nPlease Wait...");

                // Save information as new Customer
                var customer = new Customer
                {
                    Username = username,
                    Password = password
                };

                db.Customers.Add(customer);
                db.SaveChanges();
                Console.WriteLine("Changes saved! Redirecting you to Login Page...");
                Thread.Sleep(2500);
                CustomerLogin();
                
            }
        }
        private static void CustomerLogin()
        {
             Console.Clear();
            Console.WriteLine("\n        Federal Bank");
            Console.WriteLine("+--------------------------+");
            Console.WriteLine("|      CUSTOMER LOGIN      |");
            Console.WriteLine("+--------------------------+");
            Console.Write("|Username: ");
            var username = Console.ReadLine();
            Console.Write("|Password: ");
            Console.ForegroundColor = ConsoleColor.Black;
            var password = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;

            using (var db = new CustomerContext())
            {
                Customer cust = (from c in db.Customers
                                 where c.Username.Equals(username)
                                 select c).FirstOrDefault();

                if (cust == null)
                {
                    Console.WriteLine("\nInvalid username, press enter to try again");
                    Console.ReadLine();
                    CustomerLogin();
                }
                else if (!cust.Password.Equals(password))
                {
                    Console.WriteLine("\nInvalid password, press enter to try again");
                    Console.ReadLine();
                    CustomerLogin();
                }
                else
                {
                    Console.WriteLine("\nYou have successfully logged in!");
                    Console.WriteLine("\nPress enter to continue to account menu!");
                    Console.ReadLine();
                }
            }

        }

    }
}
