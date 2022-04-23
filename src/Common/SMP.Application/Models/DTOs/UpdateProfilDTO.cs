﻿using Microsoft.AspNetCore.Http;
using SMP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.DTOs
{
    public class UpdateProfilDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ImagePath { get; set; }

        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;

        public IFormFile UploadPath { get; set; }
    }
}
