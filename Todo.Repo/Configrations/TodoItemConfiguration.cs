using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Repo.Configrations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        

            // relation between TodoItem and user 
            public void Configure(EntityTypeBuilder<TodoItem> builder)
            {
               
                builder.Property(t => t.Title)
                       .IsRequired()
                       .HasMaxLength(100);

                builder.Property(t => t.UserEmail)
                       .IsRequired(); 

               
            }
        }
    }

