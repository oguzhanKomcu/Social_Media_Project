using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.VMs
{
    public  class FavoritePostVM
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public AppUser AppUser { get; set; }

        public int Post_Id { get; set; }
        public Post Post { get; set; }
        public string Total_Score { get; set; }
        public string Total_Comment { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string UserImagePath { get; set; }
        public string FullName { get; set; }


        public List<Post_Comment> Post_Comments { get; set; }
        public List<Post_Score> Post_Scores { get; set; }

    }
}
