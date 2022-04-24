using Microsoft.EntityFrameworkCore.Query;
using SMP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Domain.UoW
{
    public interface IUnitOfWork : IAsyncDisposable
    {

       

        IAppUserRepository UserRepository { get; }
        IFavoritePostRepository FavoritePostRepository { get; }
        IFollowerRepository FollowerRepository { get; }
        IHashtagRepository HashtagRepository { get; }
        IPostCommentRepository PostCommentRepository { get; }
        IPostRepository PostRepository { get; }
        IPostScoreRepository PostScoreRepository { get; }
        IPostSharingRepository PostSharingRepository { get; }
        IPageRepository PageRepository { get; }



        Task Commit();

        Task ExecuteSqlRaw(string sql, params object[] parameteres);



    }
}
