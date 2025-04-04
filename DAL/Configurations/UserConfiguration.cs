﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Login).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired();

            builder.HasMany<Report>(x => x.Reports)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.Id);

            builder.HasMany<TimeSheet>(x => x.TimeSheets)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.Id);

            //builder.HasOne<UserToken>(x => x.UserToken)
            //    .WithOne(x => x.User)
            //    .HasForeignKey(x => x.UserId)
            //    .HasPrincipalKey(x => x.Id);
        }
    }
}
