using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMP.Domain.Models.Entities;
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
        public DbSet<Post_Comment> Post_Comments { get; set; }
        public DbSet<Post_Score> Post_Scores { get; set; }
        public DbSet<PostSharing> PostSharings { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Follower>()
            .HasOne(c => c.FollowUsers)
           .WithMany(c => c.Follow)

            .OnDelete(DeleteBehavior.NoAction);
            
            builder.Entity<Follower>()
           .HasOne(c => c.FollowingUsers)
          .WithMany(c => c.Following)
    

           .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }



    }
}
