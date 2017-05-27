using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankAccountPrototype
{
    public class Account
    {
        [Key, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public decimal AccountBalance { get; set; }

        
        
        public virtual Customer Customer { get; set; }
    }

    
}
