using SMP.Domain.Models.Entities;
using SMP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Infrastructure.Repositories
{
    public class PostScoreRepository : BaseRepository<Post_Score>, IPostScoreRepository
    {
        public PostScoreRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
