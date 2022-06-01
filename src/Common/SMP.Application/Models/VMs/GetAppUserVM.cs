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
        
        
        public string Follower_Count { get; set; }
        public string Following_Count { get; set; }
        public List<GetPostVM> UserPosts { get; set; }

        
        


    }
}
