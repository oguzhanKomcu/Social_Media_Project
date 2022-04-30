using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMP.Domain.Models.Entities;
using SMP.Infrastructure.EntityTypeConfig;
using SMP.Infrastructure.SeedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Infrastructure
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<AppUser> AppUsers { get; set; }    
        public DbSet<Favorite_Post> Favorite_Posts { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Post_Comment> Post_Comments { get; set; }
        public DbSet<Post_Score> Post_Scores { get; set; }
        public DbSet<PostSharing> PostSharings { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new FavoritePostConfig());
            builder.ApplyConfiguration(new FollowerConfig());
            builder.ApplyConfiguration(new PageConfig());
            builder.ApplyConfiguration(new HashtagConfig());
            builder.ApplyConfiguration(new PageConfig());
            builder.ApplyConfiguration(new PostCommentConfig());
            builder.ApplyConfiguration(new PostConfig());
            builder.ApplyConfiguration(new PostScroreConfig());
            builder.ApplyConfiguration(new PostSharingConfig());

            builder.ApplyConfiguration(new PageSeeding());

            base.OnModelCreating(builder);
        }
      


    }
}
