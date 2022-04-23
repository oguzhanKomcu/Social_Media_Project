using SMP.Application.Extensions;
using SMP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.DTOs
{
    public class UpdatePageDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug => Title.GenerateSlug();// "GenerateSlug()" My Extensions metod
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
