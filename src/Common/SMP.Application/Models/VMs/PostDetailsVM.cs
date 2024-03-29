﻿using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.VMs
{
    public class PostDetailsVM
    {
        public int Id { get; set; }
        public string User_Id { get; set; }
        public AppUser AppUser { get; set; }
        public string? Total_Score { get; set; }
        public string? Total_Comment { get; set; }
        public string? ImagePath { get; set; }
        public string UploadPath { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string UserImagePath { get; set; }

        public decimal Score { get; set; }
        public List<PostCommentVM> Comments { get; set; }

        public DateTime CreateDate { get; set; }




        public List<Post_Comment> Post_Comments { get; set; }
        public List<Post_Score> Post_Scores { get; set; }
    }
}
