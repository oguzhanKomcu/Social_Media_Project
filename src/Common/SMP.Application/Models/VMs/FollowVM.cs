using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.VMs
{
    public  class FollowVM
    {
        public int Id { get; set; }
        public string UserName{ get; set; }

        public string Image { get; set; }
        public string? User_Id { get; set; }
        public string? User_Score { get; set; }
        public string? FollowingId { get; set; }
        public string? FollowerId { get; set; }
        public string? FollowerUserName { get; set; }





    }
}
