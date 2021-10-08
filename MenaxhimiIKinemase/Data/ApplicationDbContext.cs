using MenaxhimiIKinemase.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MenaxhimiIKinemase.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public DbSet<Orar> Orar { get; set; }
    }
}
