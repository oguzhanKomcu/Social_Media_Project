using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Domain.Models.Entities
{
    public class Post_Comment
    {
        public int Id { get; set; }
        public int Post_Id { get; set; }
        public Post Post { get; set; }
        public int User_Id { get; set; }
        public AppUser User  { get; set; }
        public string Text { get; set; }
    
    }
    
}
