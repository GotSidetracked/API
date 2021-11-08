using KTU_API.Data.Dtos.Users;
using KTU_API.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTU_API.Data
{/*
    public class RestContext : IdentityDbContext<Users>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Needed_Gear> NGear { get; set; }
        public DbSet<Path> Paths { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // !!! DON'T STORE THE REAL CONNECTION STRING THE IN PUBLIC REPO !!!
            // Use secret managers provided by your chosen cloud provider
            //= @"Server=(localdb)\mssqllocaldb;Database=myWebApp;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb; Initial Catalog=myWebApp");
        }
    }
    */
}
