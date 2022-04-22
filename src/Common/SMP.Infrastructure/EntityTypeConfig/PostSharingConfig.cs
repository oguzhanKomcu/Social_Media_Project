using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Infrastructure.EntityTypeConfig
{
   
    public class PostSharingConfig : BaseEntityConfig<PostSharing>
    {


        public override void Configure(EntityTypeBuilder<PostSharing> builder)
        {

            builder.HasKey(x => x.Id);

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
            .WithMany(x => x.PostSharings)
            .HasForeignKey(x => x.User_Id);


            builder.HasOne(x => x.Post)
            .WithMany(x => x.PostSharings)
            .HasForeignKey(x => x.Post_Id);

            base.Configure(builder);
        }
    }
}
