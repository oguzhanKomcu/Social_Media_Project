using SMP.Domain.Enums;
using SMP.Domain.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Domain.Models.Entities
{
    public class Post : IBaseEntity
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public AppUser AppUser { get; set; }
        public string  Total_Score { get; set; }
        public string  Total_Comment { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }



        
        
    }


    
}
