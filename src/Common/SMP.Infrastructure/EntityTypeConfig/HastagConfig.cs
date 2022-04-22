using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Infrastructure.EntityTypeConfig
{
    public  class HastagConfig : BaseEntityConfig<Hashtag>
    {
        public override void Configure(EntityTypeBuilder<Hashtag> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Hashtags)
                .HasForeignKey(x => x.Post_Id);


            builder.Property(x => x.Text).IsRequired(true);




            base.Configure(builder);
        }
    }
}
