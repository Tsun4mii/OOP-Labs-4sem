using Lab_11.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_11.Context
{
    public class PlaneContext : DbContext
    {
        public PlaneContext() : base("DBConnection") { }

        public DbSet<Plane> Plane { get; set; }
    }
}
