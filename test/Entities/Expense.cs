using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Entities
{
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string name { get; set; }

        [Required]
        [MaxLength(200)]
        public string description { get; set; }

        public ICollection<Transaction> Transaction = new List< Transaction > ();

    }
}
