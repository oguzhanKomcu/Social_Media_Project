using Microsoft.AspNetCore.Http;
using SMP.Domain.Enums;
using SMP.Domain.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Domain.Models.Entities
{
    public class Post : IBaseEntity
    {
        public int Id { get; set; }
        public string User_Id { get; set; }
        public AppUser AppUser { get; set; }
        public decimal Total_Score { get; set; }

        public int Total_Comment { get; set; }
        public string ImagePath { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        public List<Post_Comment> Post_Comments { get; set; }
        public List<Post_Score> Post_Scores { get; set; }
        public List<Hashtag> Hashtags { get; set; }
        public List<FavoritePost> Favorite_Posts { get; set; }
        public List<PostSharing> PostSharings { get; set; }





    }



}
