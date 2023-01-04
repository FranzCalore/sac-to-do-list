using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List
{
    public class ToDoListContext: DbContext
    {
        public DbSet<Cliente> Clienti { get; set;}
        public DbSet<Dipendente> Dipendenti { get;set;}
        public DbSet<Compito> Compiti { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compito>()
                .Property(c => c.Scadenza)
                .HasColumnType("Date");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Database=ToDoList;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
