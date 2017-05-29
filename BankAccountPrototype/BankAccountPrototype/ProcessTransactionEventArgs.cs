using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountPrototype
{
    public class ProcessTransactionEventArgs : EventArgs
    {
        public Customer Customer { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }

        public ProcessTransactionEventArgs(Customer cust, decimal amount, string transtype)
        {
            Customer = cust;
            Amount = amount;
            TransactionType = transtype;
        }
    }
}
