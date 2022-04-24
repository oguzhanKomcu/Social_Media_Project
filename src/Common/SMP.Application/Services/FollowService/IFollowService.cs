using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.FollowService
{
    public  interface IFollowService
    {
        Task Create(CreateFollowerDTO model);
        Task Delete(int id);
        Task<CreateFollowerDTO> GetById(int id);
        Task<List<FollwersVm>> GetFollowers();
        Task<List<FollowingVM>> GetFollowings();
        Task<bool> IsHashtagExsist(string text);

    }
}
