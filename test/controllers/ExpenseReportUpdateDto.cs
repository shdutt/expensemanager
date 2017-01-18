using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.controllers
{
    public class ExpenseReportUpdateDto
    {
        [Required(ErrorMessage = "Please provide a name for ExpenseReport")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
