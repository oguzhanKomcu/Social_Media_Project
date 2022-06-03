using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Infrastructure.EntityTypeConfig
{
    public  class PostConfig : BaseEntityConfig<Post>
    {
  


        public override void Configure(EntityTypeBuilder<Post> builder)
        {

            //entityfreamwork tutoriale bak öğren 

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Total_Score)
                 .HasPrecision(18, 2)
                 .HasConversion<decimal>()
                 .IsRequired();
            builder.Property(x => x.Total_Comment).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired();
            builder.Property(x => x.Description).IsRequired();


            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.User_Id);


            base.Configure(builder);
        }
    }
}
