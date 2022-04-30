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


            builder.HasOne(c => c.FollowUser)
              .WithMany(c => c.Follow)
                 .HasForeignKey(x => x.Follow_User_Id)
               .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(c => c.FollowingUser)
            .WithMany(c => c.Following)
            .HasForeignKey(x => x.Following_UserId)
            
              .OnDelete(DeleteBehavior.NoAction);


            base.Configure(builder);
        }
    }
}
