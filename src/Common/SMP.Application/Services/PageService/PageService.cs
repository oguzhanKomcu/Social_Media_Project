using AutoMapper;
using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using SMP.Domain.Enums;
using SMP.Domain.Models.Entities;
using SMP.Domain.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.PageService
{
    public class PageService : IPageService
    {



        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public PageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task Create(CreatePageDTO model)
        {
            var page = _mapper.Map<Page>(model);
            await _unitOfWork.PageRepository.Create(page);
            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var page = await _unitOfWork.PageRepository.GetDefault(x => x.Id == id);
            page.Status = Status.Passive;
            page.DeleteDate = DateTime.Now;
            await _unitOfWork.Commit();

        }

        public  async Task<UpdatePageDTO> GetById(int id)
        {
            var page = await _unitOfWork.PageRepository.GetFilteredFirstOrDefault(
                selector: x => new GetPageVM
                {
                    Id = id,
                    Title = x.Title,
                    Content = x.Content,
                    Slug = x.Slug,

                },
                expression: x => x.Status != Status.Passive &&
                             x.Id == id);
            var model = _mapper.Map<UpdatePageDTO>(page);
            return model;
        }

        public async Task<Page> GetBySlug(string slug)
        {
            var page = await _unitOfWork.PageRepository.GetDefault(x => x.Slug == slug);
            return page;
        }

        public async Task<List<GetPageVM>> GetPages()
        {
            var page = await _unitOfWork.PageRepository.GetFilteredList(
                 selector: x => new GetPageVM
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Content = x.Content,
                     Slug = x.Slug,

                 },
                 expression: x => x.Status != Status.Passive,
                 orderBy: x => x.OrderBy(x => x.Title));
            return page;
        }

        public async Task<bool> IsPageExsist(string slug)
        {
            bool isExist = await _unitOfWork.PageRepository.Any(x => x.Slug == slug);
            return isExist;

        }

        public async Task Update(UpdatePageDTO model)
        {
            var page = _mapper.Map<Page>(model);
            _unitOfWork.PageRepository.Update(page); 
            await _unitOfWork.Commit();
        }
    }
}
