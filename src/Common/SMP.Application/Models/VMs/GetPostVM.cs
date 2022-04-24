using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.VMs
{
    public class GetPostVM
    {
        public int Id { get; set; }
        public string User_Id { get; set; }
        public AppUser AppUser { get; set; }
        public decimal Total_Score { get; set; }
        public int Total_Comment { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string UserImagePath { get; set; }
        public string FullName { get; set; }
        public decimal Score { get; set; }
        public string Comment { get; set; }

        public DateTime CreateDate { get; set; }




        public List<Post_Comment> Post_Comments { get; set; }
        public List<Post_Score> Post_Scores { get; set; }




    }
}
