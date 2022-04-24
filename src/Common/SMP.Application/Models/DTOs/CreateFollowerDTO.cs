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
        public int Follow_User_Id { get; set; }



        public int Following_UserId { get; set; }



        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
