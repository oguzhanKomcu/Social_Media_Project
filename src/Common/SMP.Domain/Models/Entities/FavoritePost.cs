﻿using SMP.Domain.Enums;
using SMP.Domain.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Domain.Models.Entities
{
    public class FavoritePost :IBaseEntity
    {
        public int Id { get; set; }

        [ForeignKey("PostId")]
        public int PostId { get; set; } 
        public Post Post { get; set; }
        public string UserId { get; set; }
        public AppUser? User { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        //public List<Post_Comment> Post_Comments { get; set; }
        //public List<Post_Score> Post_Scores { get; set; }
        //public List<Post> Posts { get; set; }

    }
}
