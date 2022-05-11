using SMP.Application.Models.VMs;
using SMP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.DTOs
{
    public  class CreateFavoritePost
    {

        public int PostId { get; set; }

        public string UserId { get; set; }



        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
