using SMP.Domain.Enums;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.VMs
{
    public class GetAppUserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string? ImagePath { get; set; }
        public string Biyography { get; set; }
        public string User_Score { get; set; }
        public string Total_Post { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        
        
        public string Follower_Count { get; set; }
        public string Following_Count { get; set; }
        public List<GetPostVM> UserPosts { get; set; }
        public List<PostandPostSharingVm> SaharingPosts { get; set; }





    }
}
