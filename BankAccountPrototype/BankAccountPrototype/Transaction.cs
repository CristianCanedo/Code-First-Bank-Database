using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountPrototype
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }

        public virtual Account Account { get; set; }

    }
}
