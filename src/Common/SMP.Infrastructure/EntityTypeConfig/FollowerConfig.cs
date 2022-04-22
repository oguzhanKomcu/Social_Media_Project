using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Infrastructure.EntityTypeConfig
{
    internal class FollowerConfig : BaseEntityConfig<Follower>
    {
        public override void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.HasKey(x => x.Id);


            builder.HasOne(c => c.FollowUsers)
              .WithMany(c => c.Follow)
               .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(c => c.FollowingUsers)
            .WithMany(c => c.Following)
              .OnDelete(DeleteBehavior.NoAction);


            base.Configure(builder);
        }
    }
}
