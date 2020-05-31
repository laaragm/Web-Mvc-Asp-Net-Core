using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMvc.Models;

namespace WebMvc.Data
{
    //This is an Entity framework class (which is responsible for establishing a connection with our db).
    public class WebMvcContext : DbContext
    {
        public WebMvcContext (DbContextOptions<WebMvcContext> options)
            : base(options)
        {
        }

        //The DbSet class represents an entity set that can be used for create, read, update, and delete operations.
        //The context class (derived from DbContext) must include the DbSet type properties for the entities which
        //map to database tables and views.
        //The Migrations feature enables you to change the data model and deploy your changes to production by 
        //updating the database schema without having to drop and re-create the database.
        //We are using Migrations to create our database structure, and we are going to populate in with a SeedingService.
        //After setting up these required properties we need to call Add-Migration in order create a new Migration
        //and then call Update-Database to update our database. (Do this in Package Manager Console)
        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
