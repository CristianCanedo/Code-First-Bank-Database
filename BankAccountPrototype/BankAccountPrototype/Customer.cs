using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountPrototype
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual Account Account { get; set; }
    }

    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>()
                .HasRequired<Account>(t => t.Account)
                .WithRequiredPrincipal(t => t.Customer);

            modelBuilder.Entity<Transaction>()
                .HasRequired<Account>(t => t.Account)
                .WithMany(t => t.Transactions)
                .HasForeignKey(s => s.AccountId);

            
        }
    }
}