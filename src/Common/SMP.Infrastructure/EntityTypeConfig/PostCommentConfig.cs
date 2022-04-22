using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Infrastructure.EntityTypeConfig
{

    public class PostCommentConfig : BaseEntityConfig<Post_Comment>
    {
        public override void Configure(EntityTypeBuilder<Post_Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
            .WithMany(x => x.Post_Comments)
            .HasForeignKey(x => x.User_Id);


            builder.HasOne(x => x.Post)
            .WithMany(x => x.Post_Comments)
            .HasForeignKey(x => x.Post_Id);






            base.Configure(builder);
        }
    }
}
