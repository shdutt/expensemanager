using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.controllers
{
    public class TransactionReportCreationDto
    {
        [Required(ErrorMessage = "Please provide a name for ExpenseReport")]
        [MaxLength(50)]
        public string CustName { get; set; }
        [MaxLength(20)]
        public string DateAndTime { get; set; }

        public int ExId { get; set; }

        public int amount { get; set; }
        [MaxLength(20)]
        public string country { get; set; }
        [MaxLength(10)]
        public string type { get; set; }
        public string Uri { get; set; }
    }
}
