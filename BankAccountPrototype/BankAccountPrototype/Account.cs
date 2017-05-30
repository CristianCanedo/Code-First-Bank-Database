using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountPrototype
{
    public class Account
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }
        public int AccountId { get; set; }
        public decimal AccountBalance { get; set; }
        public string AccountType { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection <Transaction> Transactions { get; set; }
    }

}
