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
        Task Delete(string id, string userId);

        Task<List<FollowVM>> GetFollowers(string id);
        Task<List<FollowVM>> GetFollowings(string id);
        Task<bool> IsFollowExsist(CreateFollowerDTO model);
        Task<List<string>> PostFollowingControl(string id);


    }
}
