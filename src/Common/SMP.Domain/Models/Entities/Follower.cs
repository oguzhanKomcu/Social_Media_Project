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
        

        
        
        //[ForeignKey("Follow_User_Id")]
        //[InverseProperty("Followers")]
        //public AppUser FollowUsers { get; set; }
        
        
        //[ForeignKey("Follow_User_Id")]
        //[InverseProperty("Followings")]
        //public int Following_UserId { get; set; }
        //public AppUser FollowingUsers { get; set; }
        
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        
        
        public int FollowerId { get; set; }
        [ForeignKey("FollowerId")]
        [InverseProperty("Followers")]

        public AppUser Follow { get; set; }

        public int FollowingId { get; set; }
        [ForeignKey("FollowingId")]
        [InverseProperty("Followings")]
        public AppUser Following { get; set; }




    }
}
