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
    public class UpdatePostDTO
    {

        public int Id { get; set; }
        public int User_Id { get; set; }


        public string Total_Score { get; set; }
        public string Total_Comment { get; set; }
        public string ImagePath { get; set; }
        public IFormFile? UploadPath { get; set; }
        public string Description { get; set; }

        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;

        public List<PostCommentVM> Post_Comments { get; set; }
        public List<PostScoreVM> Post_Scores { get; set; }

    }
}
