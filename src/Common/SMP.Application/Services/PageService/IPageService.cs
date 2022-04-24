using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.PageService
{
    public  interface IPageService
    {
        Task Create(CreatePageDTO model);
        Task Update(UpdatePageDTO model);
        Task Delete(int id);
        Task<UpdatePageDTO> GetById(int id);
        Task<List<GetPageVM>> GetPages();
        Task<bool> IsPageExsist(string slug);
        Task<Page> GetBySlug(string slug);

    }
}
