using SMP.Application.Models.VMs;
using SMP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.DTOs
{
    public class CreatePostScoreDTO
    {

        public int Post_Id { get; set; }

        public int User_Id { get; set; }

        public decimal Score { get; set; }
        public DateTime CreateDate { get; set; }
        public Status Status { get; set; }
    }
}
