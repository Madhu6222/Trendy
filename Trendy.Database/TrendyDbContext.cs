using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendy.Entities;

namespace Trendy.Database
{
    public class TrendyDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Config> Configurations { get; set; }

        public TrendyDbContext() :base("TrendyConnection")
        {

        }
    }
}
