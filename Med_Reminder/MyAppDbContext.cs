using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Med_Reminder
{
    public class MyAppDbContext:DbContext
    {
        public DbSet<DaneOsobowe> __dane_osobowe { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-Q1NRRDR\\SQLEXPRESS;Database=Med;Trusted_Connection=True;");
        }

     

    }
}
