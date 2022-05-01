using SMP.Domain.Enums;
using SMP.Domain.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Domain.Models.Entities
{
    public  class Hashtag : IBaseEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public  string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }        
    }
}
