using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    public class InventoryContext : DbContext
    {
        
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<ValidOffice> ValidOffice { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<CategoryText> CategoryText { get; set; }

        public DbSet<CategoryProp> CategoryProp { get; set; }

        public DbSet<CategoryPropText> CategoryPropText { get; set; }

        public DbSet<InventoryExtraData> InventoryExtraData { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              "Server = (localdb)\\mssqllocaldb; Database = Mini_project_2_Anders_Wallin;");
        }
    }
}
