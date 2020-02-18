using ToDoApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace ToDoEFDB.Context
{
    public class ToDoAppContext : DbContext
    {
        public DbSet<UserItem> UserItem { get; set; }
        public DbSet<RoleItem> RoleItem { get; set; }
        public DbSet<ToDoListItem> ToDoListItem { get; set; }
        public DbSet<ToDoItem> ToDoItem { get; set; }

        public static readonly LoggerFactory MyConsoleLoggerFactory = new LoggerFactory(new[] {
            new ConsoleLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true)});

        public ToDoAppContext(DbContextOptions<ToDoAppContext> options) : base(options)
        {
        }

        public ToDoAppContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source=PREDDY-MASTER\\SQLEXPRESS;Initial Catalog=ToDoEFDB;Integrated Security=True")
                .UseLoggerFactory(MyConsoleLoggerFactory);
            //optionsBuilder.UseSqlServer("Server = (localdb)\\PREDDY-MASTER\\SQLEXPRESS; Database = ToDoEFDB; Trusted_Connection = True");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
