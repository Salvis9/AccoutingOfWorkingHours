using Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.IsActive).IsRequired();

            builder.HasMany<TimeSheet>(x => x.TimeSheets)
                .WithOne(x => x.TaskEntity)
                .HasForeignKey(x => x.TaskEntityId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
