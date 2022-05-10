using Microsoft.AspNetCore.Http;
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

        public string? Total_Score { get; set; }
        public string? Total_Comment { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? UploadPath { get; set; }
        public string? Description { get; set; }

        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Modified;

        public List<PostCommentVM>? Post_Comments { get; set; }
        public List<PostScoreVM>? Post_Scores { get; set; }

    }
}
