using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.PostService
{
    public interface IPostService
    {
        Task Create(PostDTO model);
        
        Task Update(PostDTO model);

        Task Delete(int id);

        Task<PostDTO> GetById(int id);

        Task<List<GetPostVM>> UserGetPosts(string id);
        Task<List<GetPostVM>> GetAllPostsUserName(string userName);
        Task<PostDetailsVM> GetPostDetails(int id);

        Task<List<GetPostVM>> GetPostsForMembers(string id);
    }
}
