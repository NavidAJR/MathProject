using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathProject.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathProject.DataLayer.Context
{
    public class DataBaseContext : DbContext
    {

        public DbSet<MathExpression> MathExpressions { get; set; }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=NAVID_AJR\SQL2019EXPRESS;Database=MathDB;Trusted_Connection=True;");
        }
    }
}
