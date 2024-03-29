﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Infrastructure.EntityTypeConfig
{
    public class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired(false);
            builder.Property(x => x.Biyography).IsRequired(false);
            builder.Property(x => x.User_Score).IsRequired(false);
            builder.Property(x => x.Follower_Count).IsRequired(false);
            builder.Property(x => x.Following_Count).IsRequired(false);
            builder.Property(x => x.Location).IsRequired(false);




            base.Configure(builder);
        }
    }
}
