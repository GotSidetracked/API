using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KTU_API.Data.Dtos.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KTU_API.Data.Entities
{
    public class DBContextModel : IdentityDbContext<User>
    {
        public DBContextModel(DbContextOptions<DBContextModel> options)
            : base(options)
        {
     
        }
        

        public DbSet<Comment> Comments { get; set; }
        
        public DbSet<Coordinate> Coordinates { get; set; }

        public DbSet<Needed_Gear> needed_Gears { get; set; }
        
        public DbSet<Path> paths { get; set; }
        public DbSet<User> users { get; set; }


    }
}
