using SMP.Application.Models.VMs;
using SMP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.DTOs
{
    public class CreateFollowerDTO
    {
        public int Id { get; set; }

        public string FollowerId { get; set; }



        public string FollowingId { get; set; }



        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
