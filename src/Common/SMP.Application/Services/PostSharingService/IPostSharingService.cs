using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.PostSharingService
{
    public  interface IPostSharingService
    {
        Task Create(PostSharingDTO model);
        Task Delete(int id);
        Task<List<PostandPostSharingVm>> RegisteredPostes();
        Task<bool> IsRegisteredPostExsist(int postId);

    }
}
