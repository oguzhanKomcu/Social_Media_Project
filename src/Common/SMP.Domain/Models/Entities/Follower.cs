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
        
         public int Follow_User_Id { get; set; }
        [ForeignKey("Follow_User_Id")]
        [InverseProperty("FollowUsers")]
        public AppUser FollowUser { get; set; }
        [ForeignKey("Follow_User_Id")]
        [InverseProperty("FollowingUsers")]
        public int Following_UserId { get; set; }
        public AppUser FollowingUser { get; set; }
        
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
    }
}
