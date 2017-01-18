using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Entities;

namespace test.controllers
{
    public class ExpenseReportDto 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<TransactionReportDto> Transaction { get; set; } 
        = new List<TransactionReportDto>();
        

    }
    

}


