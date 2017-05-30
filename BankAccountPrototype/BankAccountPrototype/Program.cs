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
    class Program
    {
        enum MainMenu { Admin = 1, Customer, Exit };
        static int Main()
        {
            int admin = (int)MainMenu.Admin;
            int customer = (int)MainMenu.Customer;
            int exit = (int)MainMenu.Exit;

            Console.Clear();
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

            try
            {
                int sel = int.Parse(Console.ReadLine());

                if (sel == admin)
                {
                    DisplayAdminMenu();
                }
                else if (sel == customer)
                {
                    DisplayCustomerMenu();
                }
                else if (sel == exit)
                {
                    return 0;
                }
                else
                {
                    throw new FormatException();
                }

                //switch (sel)
                //{
                //    case 1: 
                //        break;
                //    case 2: DisplayCustomerMenu();
                //        break;
                //    case 3: 
                //        return 0;
                //    default:
                //        throw new FormatException();
                //}
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid input, press [ENTER] to try again.");
                Console.ReadLine();
                Main();
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

            try
            {
                int sel = int.Parse(Console.ReadLine());

                switch (sel)
                {
                    case 1:
                    //AdminLogin()
                    //break;
                    case 2:
                        Main();
                        break;
                    case 3:
                        return 0;
                    default:
                        throw new FormatException();
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("\nInvalid input, press [ENTER] to try again");
                Console.ReadLine();
                DisplayAdminMenu();
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

            try
            {
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
                        Main();
                        break;
                    case 4:
                        return 0;
                    default:
                        throw new FormatException();
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("\nInvalid input, press [ENTER] to try again");
                Console.ReadLine();
                DisplayCustomerMenu();
            }
            return 0;
        }

        /// <summary>
        /// User creates initial customer account
        /// then is redirected to their respective account 
        /// menu
        /// </summary>
        private static void CreateCustomer()
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
                    Console.WriteLine("|1.) Back                  |");
                    Console.WriteLine("+--------------------------+");
                    Console.WriteLine("|                          |");

                    Console.Write("|First Name: ");
                    string firstName = Console.ReadLine().Trim().ToUpper();
                    if (firstName == "1") DisplayCustomerMenu(); // BACK

                    Console.Write("|Last Name: ");
                    string lastName = Console.ReadLine().Trim().ToUpper();
                    if (lastName == "1") DisplayCustomerMenu(); // BACK

                    Console.Write("|Birthdate(mm/dd/yyyy): ");
                    DateTime birthDate = DateTime.Parse(Console.ReadLine());

                    Console.Write("|New Username: ");
                    var username = Console.ReadLine().Trim().ToLower();
                    if (username == "1") DisplayCustomerMenu(); // BACK

                    // Hide passwords for security
                    Console.Write("|New Password: ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    var password = Console.ReadLine(); // save password
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (password == "1") DisplayCustomerMenu(); // BACK

                    Console.Write("|Re-enter Password: ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    var samePass = Console.ReadLine(); // save same password
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (password == "1") DisplayCustomerMenu(); // BACK

                    // Password validation
                    while (password != samePass)
                    {
                        Console.WriteLine("Passwords do not match.");
                        Console.Write("|Re-enter Password: ");
                        Console.ForegroundColor = ConsoleColor.Black;
                        samePass = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    Console.WriteLine("|                          |");
                    Console.WriteLine("+--------------------------+");
                    Console.WriteLine("\nPlease Wait, Saving Changes...");

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
                    Console.WriteLine("\nChanges saved! Press [ENTER] to navigate to Customer Login.");
                    Console.ReadLine();
                    CustomerLogin();
                }
                catch(ArgumentNullException)
                {
                    Console.WriteLine("\nCannot enter null value. Press [ENTER] to try again");
                    Console.ReadLine();
                    CreateCustomer();
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid input, press [ENTER] to try again");
                    Console.ReadLine();
                    CreateCustomer();
                }
                catch(Exception e)
                {
                    Console.WriteLine("\nInvalid input, press [ENTER] to try again");
                    Console.ReadLine();
                    CreateCustomer();
                }

            }
        }

        /// <summary>
        /// Finishing creating customer account and save changes
        /// as well as add the initial deposit transaction to 
        /// their transaction history
        /// </summary>
        private static void CreateAccount(int customerId)
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

                    Console.Write("|Account Type(Checkings): ");
                    string type = Console.ReadLine().Trim().ToUpper();

                    Console.Write("|Initial Account Deposit: $");
                    decimal amount = decimal.Parse(Console.ReadLine());
                    string tranType = "Deposit"; // Setting type
                    

                    Console.WriteLine("|                           |");
                    Console.WriteLine("+---------------------------+");

                    Console.WriteLine("\nPlease wait, saving changes...");

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
                    Console.WriteLine("\nPress [ENTER] to navigate to Account Menu");
                    Console.ReadLine();
                    DisplayAccountMenu(custId);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("\nInvalid input, press [ENTER] to try again.");
                    Console.ReadLine();
                    CreateAccount(custId);
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid input, press [ENTER] to try again.");
                    Console.ReadLine();
                    CreateAccount(custId);
                }
            }
            
        }

        /// <summary>
        /// User logs in with username and password then
        /// is redirected to AccountMenu()
        /// </summary>
        private static void CustomerLogin()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\n        Federal Bank");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|      CUSTOMER LOGIN      |");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|1.) Back                  |");
                Console.WriteLine("|--------------------------|");
                Console.Write("|Username: ");
                var username = Console.ReadLine().ToString().Trim().ToLower();

                if (username == "1")
                {
                    DisplayCustomerMenu(); // Allow user to return to previous screen
                }

                Console.Write("|Password: ");
                Console.ForegroundColor = ConsoleColor.Black;
                var password = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;

                if (password == "1")
                {
                    DisplayCustomerMenu(); // Allow user to return to previous screen
                }

                Console.WriteLine("\nPlease Wait...");
                using (var db = new CustomerContext())
                {
                    // Reach in database to find user matching username
                    Customer cust = (from c in db.Customers
                                     where c.Username.Equals(username)
                                     select c).FirstOrDefault();

                    var custId = cust.CustomerId;

                    if (cust == null)
                    {
                        Console.WriteLine("\nInvalid username, press [ENTER] to try again");
                        Console.ReadLine();
                        CustomerLogin();
                    }
                    else if (!cust.Password.Equals(password))
                    {
                        Console.WriteLine("\nInvalid password, press [ENTER] to try again");
                        Console.ReadLine();
                        CustomerLogin();
                    }
                    else
                    {
                        Console.WriteLine("You have successfully logged in!");
                        Console.WriteLine("\nPress [ENTER] to navigate to your account.");
                        Console.ReadLine();
                        DisplayAccountMenu(custId);
                    }
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("\nInvalid menu option, press [ENTER] to try again.");
                Console.ReadLine();
                CustomerLogin();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("\nInvalid username, press [ENTER] to try again");
                Console.ReadLine();
                CustomerLogin();
            }
            
        }

        private static int DisplayAccountMenu(int customerId)
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

                Console.Clear();
                Console.WriteLine("\n        Federal Bank");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|    Welcome {0}! ", cust.Username);
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|                          |");
                Console.WriteLine("|1.) Display Account Info  |");
                Console.WriteLine("|2.) Account Balance       |");
                Console.WriteLine("|3.) Deposit               |");
                Console.WriteLine("|4.) Withdraw              |");
                Console.WriteLine("|5.) Transaction History   |");
                Console.WriteLine("|6.) Account Settings      |");
                Console.WriteLine("|7.) Logout                |");
                Console.WriteLine("|                          |");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("\nWhat would you like to do?");
                Console.Write("Enter selection: ");

                try
                {
                    int sel = int.Parse(Console.ReadLine());

                    switch (sel)
                    {
                        case 1: DisplayAccountInfo(custId);
                            break;
                        case 2: DisplayAccountBalance(custId);
                            break;
                        case 3: Deposit(custId);
                            break;
                        case 4: Withdraw(custId);
                            break;
                        case 5: DisplayTransactionHistory(custId);
                            break;
                        case 6:
                            // DisplayAccountSettings()
                            break;
                        case 7:
                            Console.WriteLine("\nLogout successful. REDIRECTING you to Federal Bank Home...");
                            Thread.Sleep(3000);
                            Main();
                            break;
                        default:
                            throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid menu option, press [ENTER] to try again.");
                    Console.ReadLine();
                    DisplayAccountMenu(custId);
                }
            }
            return 0;
        }

        private static void DisplayAccountInfo(int customerId)
        {
            using (var db = new CustomerContext())
            {
                var cust = db.Customers.Find(customerId); // Find customer from database
                var custId = cust.CustomerId;

                try
                {
                    Console.Clear();
                    Console.WriteLine("\n        Federal Bank");
                    Console.WriteLine("+--------------------------+");
                    Console.WriteLine("|    Account Information   |");
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
                        DisplayAccountMenu(custId);
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid input, press [ENTER] to try again.");
                    Console.ReadLine();
                    DisplayAccountInfo(custId);
                }
            }
        }

        private static void DisplayAccountBalance(int customerId)
        {
            using (var db = new CustomerContext())
            {
                var cust = db.Customers.Find(customerId); // Find customer in database
                var custId = cust.CustomerId;

                Console.Clear();
                Console.WriteLine("\n        Federal Bank");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|      Account Balance     |");
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

                try
                {
                    int sel = int.Parse(Console.ReadLine());

                    switch (sel)
                    {
                        case 1: DisplayAccountMenu(custId);
                            break;
                        case 2: DisplayTransactionHistory(custId);
                            break;
                        default:
                            throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid input, press [ENTER] to try again.");
                    Console.ReadLine();
                    DisplayAccountBalance(custId);
                }
            }
        }

        private static void Deposit(int customerId)
        {
            using (var db = new CustomerContext())
            {
                var cust = db.Customers.Find(customerId);
                var custId = cust.CustomerId;
                var bal = cust.Account.AccountBalance;

                string type = "Deposit"; // Transaction Type

                try
                {
                    Console.Clear();
                    Console.WriteLine("\n        Federal Bank");
                    Console.WriteLine("+--------------------------+");
                    Console.WriteLine("|       DEPOSIT MENU       |");
                    Console.WriteLine("+--------------------------+");
                    Console.WriteLine("|1.) Back                  |");
                    Console.WriteLine("+--------------------------+");
                    Console.WriteLine("|                          |");

                    Console.Write("|Enter amount to deposit: $");
                    decimal amount = decimal.Parse(Console.ReadLine()); // Transaction Amount
                    Console.WriteLine("|                          |");
                    Console.WriteLine("+--------------------------+");

                    if (amount == 1)
                    {
                        DisplayAccountMenu(custId);
                    }
                    else
                    {
                        Console.WriteLine("\nPlease Wait, Saving Changes...");
                    }

                    MakeTransaction(custId, amount, type);

                    Console.WriteLine("Changes have been saved successfully!");
                    Console.Write("\nPress [ENTER] to return to Account Menu.");
                    Console.ReadLine();
                    DisplayAccountMenu(custId);

                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid input, press [ENTER] to try again.");
                    Console.ReadLine();
                    Deposit(custId);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("\nInvalid input, press [ENTER] to try again.");
                    Console.ReadLine();
                    Deposit(custId);
                }
                
            }

            
        }

        private static void Withdraw(int customerId)
        {
            using (var db = new CustomerContext())
            {
                var cust = db.Customers.Find(customerId);
                var custId = cust.CustomerId;
                var bal = cust.Account.AccountBalance;

                string type = "Withdraw"; // Transaction Type

                try
                {
                    Console.Clear();
                    Console.WriteLine("\n        Federal Bank");
                    Console.WriteLine("+--------------------------+");
                    Console.WriteLine("|      WITHDRAW MENU       |");
                    Console.WriteLine("+--------------------------+");
                    Console.WriteLine("|1.) Back                  |");
                    Console.WriteLine("+--------------------------+");
                    Console.WriteLine("|                          |");

                    Console.Write("|Enter amount to withdraw: $");
                    decimal amount = decimal.Parse(Console.ReadLine()); // Transaction Amount
                    Console.WriteLine("|                          |");
                    Console.WriteLine("+--------------------------+");

                    if (amount == 1)
                    {
                        DisplayAccountMenu(custId);
                    }
                    else
                    {
                        Console.WriteLine("\nPlease Wait, Saving Changes...");
                    }

                    MakeTransaction(custId, amount, type);

                    Console.WriteLine("Changes have been saved successfully!");
                    Console.Write("\nPress [ENTER] to return to Account Menu.");
                    Console.ReadLine();
                    DisplayAccountMenu(custId);

                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid input, press [ENTER] to try again.");
                    Console.ReadLine();
                    Deposit(custId);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("\nInvalid input, press [ENTER] to try again.");
                    Console.ReadLine();
                    Deposit(custId);
                }

            }


        } 

        private static void DisplayTransactionHistory(int customerId)
        {
            using (var db = new CustomerContext())
            {
                var cust = db.Customers.Find(customerId); // Find customer in Customer table
                var custId = cust.CustomerId;

                var transactions = cust.Account.Transactions; // Easily readable variable
                int j = 1;

                Console.Clear();
                Console.WriteLine("\n        Federal Bank");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|       Transactions       |");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("|                          |");
                Console.WriteLine("|  # of Transactions: {0}    |", cust.Account.Transactions.Count());
                Console.WriteLine("|--------------------------|");
                foreach (var item in transactions)
                {
                    Console.WriteLine("|*Transaction {0}:\n   (Type: {1}, Amount: {2}, Date: {3})",
                                        j, item.TransactionType, item.Amount, item.Date);
                    j++;
                }
                Console.WriteLine("|                          |");
                Console.WriteLine("|--------------------------|");
                Console.WriteLine("|1.) Back                  |");
                Console.WriteLine("+--------------------------+");
                Console.WriteLine("\nWhat would you like to do?");
                Console.Write("Enter selection: ");
                try
                {
                    int sel = int.Parse(Console.ReadLine());
                    if (sel == 1)
                    {
                        DisplayAccountMenu(cust.CustomerId);
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid input, press [ENTER] to try again.");
                    Console.ReadLine();
                    DisplayTransactionHistory(custId);
                }
            }
        }

        private static void MakeTransaction(int customerId, decimal amount, string tranType)
        {
            using (var db = new CustomerContext())
            {
                var cust = db.Customers.Find(customerId); // Finds customer
                var custId = cust.CustomerId;

                // Get current balance
                Account currentBal = (from a in db.Accounts
                                      where a.AccountId.Equals(custId)
                                      select a).FirstOrDefault();

                // If incoming type is Deposit, add
                // If incoming type is Withdraw, subtract
                if (tranType == "Deposit")
                {
                    currentBal.AccountBalance += amount;
                    db.SaveChanges();

                    var deposit = new Transaction
                    {
                        Account = cust.Account,
                        TransactionType = tranType,
                        Amount = amount,
                        Date = DateTime.Now
                    };

                    cust.Account.Transactions.Add(deposit);
                    db.Transactions.Add(deposit);
                    db.SaveChanges();
                }
                else if (tranType == "Withdraw")
                {
                    currentBal.AccountBalance -= amount;
                    db.SaveChanges();

                    var withdraw = new Transaction
                    {
                        Account = cust.Account,
                        TransactionType = tranType,
                        Amount = -amount,
                        Date = DateTime.Now
                    };

                    cust.Account.Transactions.Add(withdraw);
                    db.Transactions.Add(withdraw);
                    db.SaveChanges();
                }
                
            }
        }
    }
}
