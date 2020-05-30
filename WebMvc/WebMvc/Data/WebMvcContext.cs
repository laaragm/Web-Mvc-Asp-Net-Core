using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMvc.Models;

namespace WebMvc.Data
{
    public class WebMvcContext : DbContext
    {
        public WebMvcContext (DbContextOptions<WebMvcContext> options)
            : base(options)
        {
        }

        public DbSet<WebMvc.Models.Department> Department { get; set; }
    }
}
