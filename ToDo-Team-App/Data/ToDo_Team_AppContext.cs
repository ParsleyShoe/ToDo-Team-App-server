using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo_Team_App.Models;

namespace ToDo_Team_App.Data
{
    public class ToDo_Team_AppContext : DbContext
    {
        public ToDo_Team_AppContext (DbContextOptions<ToDo_Team_AppContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder model) {
            model.Entity<User>(e => {
                e.ToTable("Users");
                e.HasIndex(x => x.Username).IsUnique();
            });
        }
    }
}
