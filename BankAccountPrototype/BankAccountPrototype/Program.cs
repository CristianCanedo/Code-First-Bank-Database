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
    public delegate void ProcessTransactionEventHandler(int custId, decimal amount, string tranType);

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
                case 1: DisplayAdminMenu(); break;
                case 2: DisplayCustomerMenu(); break;
                case 3: return 0;
                default:
                    Console.WriteLine("Invalid input, press enter to try again");
                    Console.ReadLine();
                    Main();
                    break;
            }

            return 0;
        }

        /// <summary>
        /// TODO: Complete
        /// </summary>
        private static int DisplayAdminMenu()
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
                    DisplayAdminMenu();
                    break;
            }

            return 0;
        }
        
        /// <summary>
        /// Displays customer main menu where user can
        /// navigate to different areas of the program
        /// as a customer
        /// </summary>
        private static int DisplayCustomerMenu()
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
                    DisplayCustomerMenu();
                    break;
            }

            return 0;
        }

        /// <summary>
        /// User creates initial customer account
        /// then is redirected to their respective account 
        /// menu
        /// </summary>
        public static void CreateCustomer()
        {
            using (var db = new CustomerContext())
            {
                try
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

                    // Hide passwords for security
                    Console.Write("|New Password: ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    var password = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Gray;

                    Console.Write("|Re-enter Password: ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    var samePass = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Gray;

                    // Password validation
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

                    // Save new account with customer
                    var custAccount = new Account
                    {
                        Customer = customer
                    };

                    // Save changes
                    db.Customers.Add(customer);
                    db.Accounts.Add(custAccount);
                    db.SaveChanges();

                    // Navigate user to Login Page
                    Console.WriteLine("Changes saved! REDIRECTING you to Login Page...");
                    Thread.Sleep(2500);
                    CustomerLogin();
                }
                catch(ArgumentNullException)
                {
                    Console.WriteLine("Cannot enter null value. Press enter to try again");
                    Console.ReadLine();
                    CreateCustomer();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid format entered. PRess enter to try again");
                    Console.ReadLine();
                    CreateCustomer();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press enter to try again.");
                    Console.ReadLine();
                    CreateCustomer();
                }

            }
        }

        /// <summary>
        /// User logs in with username and password then
        /// is redirected to AccountMenu()
        /// </summary>
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
                    Console.WriteLine("\n(*Press enter to navigate to your account*)");
                    Console.ReadLine();
                    DisplayAccountMenu(custId);
                }
            }


        }
        
        public static int DisplayAccountMenu(int customerId)
        {
            using (var db = new CustomerContext())
            {
                Customer cust = db.Customers.Find(customerId);

                string username = cust.Username;
                int custId = cust.CustomerId;
                string accountType = cust.Account.AccountType;

                // If user is a first time user, must CreateAccount
                if (accountType == null)
                {
                    CreateAccount(custId);
                }
                else { };

                Console.Clear();
                Console.WriteLine("\n        Federal Bank");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|    Welcome {0}! ", cust.Username);
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|                          |");
                Console.WriteLine("|1.) Display Account       |");
                Console.WriteLine("|2.) Account Balance       |");
                Console.WriteLine("|3.) Deposit               |");
                Console.WriteLine("|4.) Withdraw              |");
                Console.WriteLine("|5.) Transaction History   |");
                Console.WriteLine("|6.) Logout                |");
                Console.WriteLine("|                          |");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("\nWhat would you like to do?");
                Console.Write("Enter selection: ");
                int sel = int.Parse(Console.ReadLine());

                switch (sel)
                {
                    case 1: DisplayAccountInfo(custId);
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
                        Console.WriteLine("Logout successful. Taking you to Federal Bank Home.");
                        Thread.Sleep(3000);
                        Console.Clear();
                        Main();
                        break;
                }
            }
            return 0;
            
        }

        /// <summary>
        /// Finishing creating customer account and save changes
        /// as well as add the initial deposit transaction to 
        /// their transaction history
        /// </summary>
        public static void CreateAccount(int customerId)
        {
            using (var db = new CustomerContext())
            {
                Customer cust = db.Customers.Find(customerId);
                
                var custId = cust.CustomerId;
                var acc = cust.Account;

                try
                {
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
                    string tranType = "Deposit";

                    Console.WriteLine("|                           |");
                    Console.WriteLine("+---------------------------+\n");

                    Console.WriteLine("Please wait, saving changes...\n");

                    // Save changes to account if account type is null
                    if (cust.Account.AccountType == null)
                    {
                        cust.Account.AccountType = type;
                        db.SaveChanges();
                    }

                    // Save changes by calling MakeTransaction
                    MakeTransaction(custId, amount, tranType);

                    // Navigate to AccountMenu()
                    Console.WriteLine("Changes have been saved successfully!");
                    Console.WriteLine("(*Press ENTER to navigate to account menu*)");
                    Console.ReadLine();
                    DisplayAccountMenu(custId);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Invalid input, press enter to try again.");
                    Console.ReadLine();
                    CreateAccount(custId);
                }
                
            }

            // Add event to be raised for transaction
        }

        public static void DisplayAccountInfo(int customerId)
        {
            using (var db = new CustomerContext())
            {
                var cust = db.Customers.Find(customerId); // Find customer from database

                try
                {
                    Console.Clear();
                    Console.WriteLine("\n        Federal Bank");
                    Console.WriteLine("+--------------------------+");
                    Console.WriteLine("|    {0}'s Account", cust.Username);
                    Console.WriteLine("+--------------------------+");
                    Console.WriteLine("|                          |");
                    Console.WriteLine("|Account ID: {0}", cust.Account.AccountId);
                    Console.WriteLine("|Account Type: {0}", cust.Account.AccountType);
                    Console.WriteLine("|First Name: {0}", cust.FirstName);
                    Console.WriteLine("|Last Name: {0}", cust.LastName);
                    Console.WriteLine("|Birth Date: {0}", cust.BirthDate.ToShortDateString());
                    Console.WriteLine("|# of Transactions: {0}", cust.Account.Transactions.Count());
                    Console.WriteLine("|                          |");
                    Console.WriteLine("+--------------------------+");
                    Console.WriteLine("|1.) Back                  |");
                    Console.WriteLine("+--------------------------+");
                    Console.WriteLine("\nWhat would you like to do?");
                    Console.Write("Enter selection: ");
                    int sel = int.Parse(Console.ReadLine());

                    if (sel == 1)
                    {
                        DisplayAccountMenu(cust.CustomerId);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, press enter to try again.");
                        Console.ReadLine();
                        DisplayAccountInfo(cust.CustomerId);
                    }
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Cannot enter null value. Press enter to try again.");
                    Console.ReadLine();
                    DisplayAccountInfo(cust.CustomerId);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press enter to try again.");
                    Console.ReadLine();
                    DisplayAccountInfo(cust.CustomerId);
                }
            }
        }

        public static void DisplayAccountBalance(int customerId)
        {
            using (var db = new CustomerContext())
            {
                var cust = db.Customers.Find(customerId);

                Console.Clear();
                Console.WriteLine("\n        Federal Bank");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|  {0}'s Account Balance", cust.Username);
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|                          |");
                Console.WriteLine("|Account ID: {0}", cust.Account.AccountId);
                Console.WriteLine("|Account Type: {0}", cust.Account.AccountType);
                Console.WriteLine("|Account Balance: {0}", cust.Account.AccountBalance);
                Console.WriteLine("|# of Transactions: {0}", cust.Account.Transactions.Count());
                Console.WriteLine("|                          |");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|1.) Back                  |");
                Console.WriteLine("|2.) Transaction History   |");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("\nWhat would you like to do?");
                Console.Write("Enter selection: ");
                int sel = int.Parse(Console.ReadLine());
            }
        }

        public static void MakeTransaction(int custId, decimal amount, string tranType)
        {
            using (var db = new CustomerContext())
            {
                var cust = db.Customers.Find(custId); // Finds customer

                var bal = cust.Account.AccountBalance;

                // If account balance is not 0, we may add to it
                // else, initialize it
                if (bal != 0)
                {
                    bal += amount;
                    db.SaveChanges();
                }
                else if (bal == 0)
                {
                    bal = amount;
                    db.SaveChanges();
                }

                // Creating a new transaction
                var trans = new Transaction
                {
                    Account = cust.Account,
                    TransactionType = tranType,
                    Amount = amount
                };

                // Save transaction
                cust.Account.Transactions.Add(trans);
                db.Transactions.Add(trans);
                db.SaveChanges();
            }
        }
    }
}
