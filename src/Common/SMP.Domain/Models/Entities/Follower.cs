using SMP.Domain.Enums;
using SMP.Domain.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Domain.Models.Entities
{
    public class Follower :IBaseEntity
    {
        public int Id { get; set; }
        
       //public int Follow_User_Id { get; set; }
        public AppUser FollowUsers { get; set; }

      //public int Following_UserId { get; set; }
        public AppUser FollowingUsers { get; set; }
        
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
    }
}
