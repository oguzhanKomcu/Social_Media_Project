using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.HashtagService
{
    public  interface IHashtagService
    {


        Task Create(CreateHashtagDTO model);
        Task Delete(int id);
        Task<CreateHashtagDTO> GetById(int id);        
        Task<List<HashtagVM>> GetHashtags();
        Task<bool> IsHashtagExsist(string text);        


    }
}
