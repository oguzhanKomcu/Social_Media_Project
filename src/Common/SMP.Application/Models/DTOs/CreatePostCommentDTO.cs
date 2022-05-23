using SMP.Application.Models.VMs;
using SMP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.DTOs
{
    public  class CreatePostCommentDTO
    {
        public int Id { get; set; }
        public int Post_Id { get; set; }

        public string User_Id { get; set; }

        public string Text { get; set; } // hasmalenght 300     
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;

    }
}
