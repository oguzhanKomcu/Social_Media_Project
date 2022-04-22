using SMP.Domain.Models.Entities;
using SMP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Infrastructure.Repositories
{
    public class PostCommentRepository : BaseRepository<Post_Comment>, IPostCommentRepository
    {
        public PostCommentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }

}
