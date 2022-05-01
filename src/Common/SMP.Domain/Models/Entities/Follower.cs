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
        

        
        
       
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }


        [ForeignKey("Follow")]
        public string FollowerId { get; set; }
        public AppUser Follow { get; set; }

        

       [ForeignKey("Following")]
        public string FollowingId { get; set; }
        public AppUser Following { get; set; }




    }
}
