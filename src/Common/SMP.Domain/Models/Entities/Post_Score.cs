﻿using SMP.Domain.Enums;
using SMP.Domain.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Domain.Models.Entities
{
    public class Post_Score : IBaseEntity
    {
        public int Id { get; set; }
        public int Post_Id { get; set; }
        public Post Post { get; set; }
        public int User_Id { get; set; }
        public AppUser User { get; set; }
        public decimal Score { get; set; } // hasmalenght 300     
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

    }
}