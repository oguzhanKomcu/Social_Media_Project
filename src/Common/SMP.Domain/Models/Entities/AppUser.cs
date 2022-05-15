

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SMP.Domain.Enums;
using SMP.Domain.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Domain.Models.Entities
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        public AppUser()
        {
            Posts = new List<Post>();
            PostSharings = new List<PostSharing>();
            Post_Scores = new List<Post_Score>();
            Favorite_Posts = new List<FavoritePost>();
            Followers = new List<Follower>();
            Followings = new List<Follower>();
            Post_Comments = new List<Post_Comment>();
        }

        public string Location { get; set; }
        [NotMapped]
        public IFormFile UploadPath { get; set; }
        public string ImagePath { get; set; }
        public string Biyography { get; set; }
        public string User_Score { get; set; }
        public string Follower_Count { get; set; }
        public string Following_Count { get; set; }

        public DateTime CreateDate { get ; set ; }
        public DateTime? UpdateDate { get; set ; }
        public DateTime? DeleteDate { get ; set; }
        public Status Status { get ; set; }
        
        [InverseProperty("Follow")]
        public List<Follower> Followers { get; set; }

        [InverseProperty("Following")]

        public List<Follower> Followings { get; set; }


        public List<Post_Score> Post_Scores { get; set; }
        public List<FavoritePost> Favorite_Posts { get; set; }
        public List<Post_Comment> Post_Comments { get; set; }
        public List<PostSharing> PostSharings { get; set; }
        public List<Post> Posts { get; set; }






    }
}
