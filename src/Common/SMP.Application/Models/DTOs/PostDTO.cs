﻿using Microsoft.AspNetCore.Http;
using SMP.Application.Models.VMs;
using SMP.Domain.Enums;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.DTOs
{
    public class PostDTO
    {

        public int Id { get; set; }
        public string? User_Id { get; set; }

        public decimal? Total_Score { get; set; }
        public int? Total_Comment { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? UploadPath { get; set; }
        public string? Description { get; set; }

        public DateTime? UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        
        public Status Status { get; set; }
        public List<PostCommentVM>? Post_Comments { get; set; }
        public List<PostScoreVM>? Post_Scores { get; set; }

    }
}
