using SMP.Domain.Enums;
using SMP.Domain.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Domain.Models.Entities
{
    public class Message :IBaseEntity
    {
        public int Id { get; set; }

        public int Message_Send_Id { get; set; }
        public AppUser User { get; set; }

        public int Message_Outgoing_Id { get; set; }
        public AppUser Following_User { get; set; }
        
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

    }
}
