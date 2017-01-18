using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.Entities
{
    public class ExpenseInfo :DbContext
    {
        public ExpenseInfo(DbContextOptions<ExpenseInfo> options)
            : base(options)
        {
            try
            {
                Database.EnsureCreated();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public DbSet<Expense> Expense { get; set; }

        public DbSet<Transaction> Transaction { get; set; }
         public DbSet<users> users { get; set; }
         
    }
}
