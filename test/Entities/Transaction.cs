using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Entities
{
    public class Transaction
    {     
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tid { get; set; }
        public string CustName { get; set; }
        public string type { get; set; }
        public String DateAndTime { get; set; }
        public int amount { get; set; }
        public string country { get; set; }
        public string Uri { get; set; }

        [ForeignKey("ExId")]
        public Expense Expense { get; set; }
        public int ExId { get; set; }
    }
}
