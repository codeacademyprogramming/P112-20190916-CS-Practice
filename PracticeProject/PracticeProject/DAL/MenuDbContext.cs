using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PracticeProject.Models;

namespace PracticeProject.DAL
{
    class MenuDbContext : DbContext
    {
        public MenuDbContext() : base("MenuDbConnectionString")
        {
        }

        public DbSet<MenuCategory> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
    }
}
