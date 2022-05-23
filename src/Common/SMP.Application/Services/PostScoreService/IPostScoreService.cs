using SMP.Application.Models.DTOs;

namespace SMP.Application.Services.PostScoreService
{
    public interface IPostScoreService
    {
        Task Create(PostScoreDTO model);
        Task<bool> IsScoreExsist(int postId, string id);
    }

}