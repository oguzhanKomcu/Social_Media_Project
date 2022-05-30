using SMP.Application.Models.VMs;
using SMP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.DTOs
{
    public class UpdatePostCommentDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; } // hasmalenght 300     
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
