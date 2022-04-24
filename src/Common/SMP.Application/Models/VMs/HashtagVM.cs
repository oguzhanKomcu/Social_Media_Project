using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Models.VMs
{
    public class HashtagVM
    {
        public int Id { get; set; }
        public int Post_Id { get; set; }

        public string Text { get; set; }
    }
}
