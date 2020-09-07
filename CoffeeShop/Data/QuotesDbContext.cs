using CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Data
{
    public class QuotesDbContext:DbContext
    {
        public QuotesDbContext(DbContextOptions<QuotesDbContext>options):base(options)

        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-EJHPO85\SQLEXPRESS; Database=Quotes;Trusted_Connection=True");
        //}
        public DbSet<Quote> Quotes { get; set; }
    }
}
