using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace BankAccountPrototype
{
    // TODO: Implement functioning event to process transactions made
    public delegate void ProcessTransactionEventHandler(ProcessTransactionEventArgs e);

    class Program
    {
        public event ProcessTransactionEventHandler ProcessTransaction;

        ProcessTransactionEventHandler transactionDel =
        new ProcessTransactionEventHandler(MakeTransaction);

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
                    //AdminLogin()
                    //break;
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

                Console.Write("|First Name: ");
                string firstName = Console.ReadLine().Trim().ToUpper();

                Console.Write("|Last Name: ");
                string lastName = Console.ReadLine().Trim().ToUpper();

                Console.Write("|Birthdate(8/4/1995): ");
                DateTime birthDate = DateTime.Parse(Console.ReadLine());

                Console.Write("|New Username: ");
                var username = Console.ReadLine().Trim().ToLower();

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
                Console.WriteLine("\n\nPlease Wait, Saving Changes...");

                // Save information as new Customer
                var customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BirthDate = birthDate,
                    Username = username,
                    Password = password,
                };

                var custAccount = new Account
                {
                    Customer = customer
                };

                db.Customers.Add(customer);
                db.Accounts.Add(custAccount);
                db.SaveChanges();
                Console.WriteLine("Changes saved! REDIRECTING you to Login Page...");
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
            var username = Console.ReadLine().Trim().ToLower();
            Console.Write("|Password: ");
            Console.ForegroundColor = ConsoleColor.Black;
            var password = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;

            using (var db = new CustomerContext())
            {
                Customer cust = (from c in db.Customers
                                 where c.Username.Equals(username)
                                 select c).FirstOrDefault();

                var custId = cust.CustomerId;

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
                    AccountMenu(custId);
                }
            }


        }
        
        public static int AccountMenu(int customerId)
        {
            using (var db = new CustomerContext())
            {
                Customer cust = db.Customers.Find(customerId);

                string username = cust.Username;
                int custId = cust.CustomerId;
                string accountType = cust.Account.AccountType;

                if (accountType == null)
                {
                    CreateAccount(custId);
                }
                else { };

                Console.Clear();
                Console.WriteLine("\n        Federal Bank");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine($"|    Welcome {username}!   ");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|                          |");
                Console.WriteLine("|1.) Account Info          |");
                Console.WriteLine("|2.) Account Balance       |");
                Console.WriteLine("|3.) Deposit               |");
                Console.WriteLine("|4.) Withdraw              |");
                Console.WriteLine("|5.) Transaction History   |");
                Console.WriteLine("|6.) Logout                |");
                Console.WriteLine("|                          |");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("\n\nWhat would you like to do?");
                Console.Write("Enter selection: ");
                int sel = int.Parse(Console.ReadLine());

                switch (sel)
                {
                    case 1: //AccountInfo()
                        break;
                    case 2: //AccountBalance()
                        break;
                    case 3: //Deposit()
                        break;
                    case 4: //Withdraw()
                        break;
                    case 5: //TransactionHistory()
                        break;
                    case 6:
                        Console.WriteLine("Logging you out please wait...");
                        db.Dispose();
                        Console.WriteLine("Logout successful. Taking you to Federal Bank Home.");
                        Thread.Sleep(3000);
                        Main();
                        break;
                }
            }
            return 0;
            
        }

        public static void CreateAccount(int customerId)
        {
            using (var db = new CustomerContext())
            {
                Customer cust = db.Customers.Find(customerId);
                
                var custId = cust.CustomerId;
                var acc = cust.Account;
                

                // Create Account for Customer
                Console.Clear();
                Console.WriteLine("\n        Federal Bank");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|   NEW CUSTOMER ACCOUNT   |");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|                          |");

                Console.Write("|Account Type(checkings): ");
                string type = Console.ReadLine().Trim().ToUpper();

                Console.Write("|Initial Account Deposit: $");
                decimal amount = decimal.Parse(Console.ReadLine());
                string transType = Console.ReadLine().Trim().ToUpper();


                Console.WriteLine("|                           |");
                Console.WriteLine("+---------------------------+\n");

                Console.WriteLine("Please wait, saving changes...\n");

                if (cust.Account.AccountType == null)
                {
                    cust.Account.AccountType = type;
                    db.SaveChanges();
                }

                if (cust.Account.AccountBalance == 0)
                {
                    cust.Account.AccountBalance = amount;
                    db.SaveChanges();
                }

                var initial = new Transaction
                {
                    Account = cust.Account,
                    TransactionType = transType,
                    Amount = amount
                };

                cust.Account.Transactions.Add(initial);
                db.Transactions.Add(initial);
                db.SaveChanges();
                

                Console.WriteLine("Changes have been saved successfully!");
                Console.WriteLine("Press enter to navigate to your account menu!");
                Console.ReadLine();
                AccountMenu(custId);
            }

            // EVENTS DELEGATES
            
            
        }

        public static void MakeTransaction(ProcessTransactionEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static void OnProcessTransaction(ProcessTransactionEventArgs e)
        {
            
        }
    }
}
