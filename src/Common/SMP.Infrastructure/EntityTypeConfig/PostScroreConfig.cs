using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Infrastructure.EntityTypeConfig
{
    public class PostScroreConfig : BaseEntityConfig<Post_Score>
    {
        public int Id { get; set; }
        public int Post_Id { get; set; }
        public Post Post { get; set; }
        public int User_Id { get; set; }
        public AppUser User { get; set; }
        public decimal Score { get; set; } // hasmalenght 300   

        public override void Configure(EntityTypeBuilder<Post_Score> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Score)
                .HasPrecision(18, 5)
                .HasConversion<decimal>()
                .IsRequired();

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Post_Scores)
                .HasForeignKey(x => x.Post_Id);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Post_Scores)
                .HasForeignKey(x => x.User_Id);

            base.Configure(builder);
        }
    }
}
