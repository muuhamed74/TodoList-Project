using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Repo.Configrations;

namespace Todo.Repo
{
   
        public class TodoDbContext : DbContext
        {
            public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
            {
            }

            public DbSet<TodoItem> TodoItems { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
              base.OnModelCreating(modelBuilder);

              
            }





        //all of the relations in TodoItemConfiguration 


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<TodoItem>()
        //        .HasOne(t => t.User)
        //        .WithMany()
        //        .HasForeignKey(t => t.UserId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}

