using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.controllers
{
   public class TransactionReportDto
    {
        public int tid { get; set; }

        public string CustName { get; set; }

        public string DateAndTime { get; set; }
        public int ExId { get; set; }
        public int amount { get; set; }
        public string country { get; set; }
        public string type { get; set; }
        public string Uri { get; set; }
    }
}
