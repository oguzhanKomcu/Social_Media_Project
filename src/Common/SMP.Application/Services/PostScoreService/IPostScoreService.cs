using SMP.Application.Models.DTOs;
using SMP.Domain.Models.Entities;

namespace SMP.Application.Services.PostScoreService
{
    public interface IPostScoreService
    {
        Task Create(PostScoreDTO model, Post post, AppUser appUser);
        Task<bool> IsScoreExsist(int postId, string id);
    }

}