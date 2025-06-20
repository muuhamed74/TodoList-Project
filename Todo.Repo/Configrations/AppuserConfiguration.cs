using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Entities.Identity;

namespace Todo.Repo.Configrations
{
    public class AppuserConfiguratio : IEntityTypeConfiguration<Appuser>
    {
        public void Configure(EntityTypeBuilder<Appuser> builder)
        {
            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
