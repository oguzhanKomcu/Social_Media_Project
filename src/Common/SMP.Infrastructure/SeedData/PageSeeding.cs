using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Infrastructure.SeedData
{
    public class PageSeeding : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasData(
                new Page { Id = 1, Title = "Home", Content = "Home", Slug = "home", CreateDate = DateTime.Now, Status = Domain.Enums.Status.Active },
                new Page { Id = 2, Title = "Profil", Content = "Profil", Slug = "profil", CreateDate = DateTime.Now, Status = Domain.Enums.Status.Active },
                 new Page { Id = 3, Title = "Favorite Post", Content = "Favorite Post", Slug = "favorite-post", CreateDate = DateTime.Now, Status = Domain.Enums.Status.Active });

        }
    }
}
