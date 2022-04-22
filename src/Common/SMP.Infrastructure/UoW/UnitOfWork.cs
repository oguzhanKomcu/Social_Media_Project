using Microsoft.EntityFrameworkCore;
using SMP.Domain.Repositories;
using SMP.Domain.UoW;
using SMP.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _appDbContext; 
        
        public UnitOfWork(AppDbContext db)
        {
            _appDbContext = db ?? throw new ArgumentNullException($"{nameof(db)} can't be a null");
            //If the incoming data is null, the right ArgumentNullException will be thrown.
        }

        private IAppUserRepository _appUserRepository;
        public IAppUserRepository UserRepository
        {
            get
            {
                if (_appUserRepository == null)
                {
                    _appUserRepository = new AppUserRepository(_appDbContext);
                }
                return _appUserRepository;
            }
        }

        private IFavoritePostRepository _favoritePostRepository;
        public IFavoritePostRepository FavoritePostRepository
        {
            get
            {
                if (_favoritePostRepository == null)
                {
                    _favoritePostRepository = new FavoritePostRepository(_appDbContext);
                }
                return _favoritePostRepository;
            }
        }

        private IFollowerRepository _followerRepository;
        public IFollowerRepository FollowerRepository
        {
            get
            {
                if (_followerRepository == null)
                {
                    _followerRepository = new FollowerRepository(_appDbContext);
                }
                return _followerRepository;
            }
        }

        private IHashtagRepository _hashtagRepository;
        public IHashtagRepository HashtagRepository
        {
            get
            {
                if (_hashtagRepository == null)
                {
                    _hashtagRepository = new HashtagRepository(_appDbContext);
                }
                return _hashtagRepository;
            }
        }

        IPostCommentRepository _postCommentRepository;
        public IPostCommentRepository PostCommentRepository
        {
            get
            {
                if (_postCommentRepository == null)
                {
                    _postCommentRepository = new PostCommentRepository(_appDbContext);
                }
                return _postCommentRepository;
            }

        }


        private IPostRepository _postRepository;
        public IPostRepository PostRepository
        {
            get
            {
                if (_postRepository == null)
                {
                    _postRepository = new PostRepository(_appDbContext);
                }
                return _postRepository;
            }
        }


        private IPostScoreRepository _postScoreRepository;
        public IPostScoreRepository PostScoreRepository
        {
            get
            {
                if (_postScoreRepository == null)
                {
                    _postScoreRepository = new PostScoreRepository(_appDbContext);
                }
                return _postScoreRepository;
            }
        }
        private IPostSharingRepository _postSharingRepository;
        public IPostSharingRepository PostSharingRepository
        {
            get
            {
                if (_postSharingRepository == null)
                {
                    _postSharingRepository = new PostSharingRepository(_appDbContext);
                }
                return _postSharingRepository;
            }
        }

      

        public async Task Commit()
        {
            await _appDbContext.SaveChangesAsync();
        }

        private bool _isDisposed = false;
        public async ValueTask DisposeAsync()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this);
            }
        }
        private async Task DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await _appDbContext.DisposeAsync();
            }
        }

        public async Task ExecuteSqlRaw(string sql, params object[] parameteres)
        {
            await _appDbContext.Database.ExecuteSqlRawAsync(sql, parameteres); //ExecuteSqlRawAsync efcore ver

        }
    }
}
