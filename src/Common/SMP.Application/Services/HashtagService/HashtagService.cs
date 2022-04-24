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

namespace SMP.Application.Services.HashtagService
{
    public class HashtagService : IHashtagService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HashtagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(CreateHashtagDTO model)
        {

            var page = _mapper.Map<Hashtag>(model);
            await _unitOfWork.HashtagRepository.Create(page);
            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var page = await _unitOfWork.HashtagRepository.GetDefault(x => x.Id == id);
            page.Status = Status.Passive;
            page.DeleteDate = DateTime.Now;
            await _unitOfWork.Commit();

        }

        public async Task<CreateHashtagDTO> GetById(int id)
        {

            var page = await _unitOfWork.HashtagRepository.GetFilteredFirstOrDefault(
                selector: x => new HashtagVM
                {
                    Id = id,
                    Post_Id = x.Post_Id,
                    Text = x.Text,

                },
                expression: x => x.Status != Status.Passive &&
                             x.Id == id);
            var model = _mapper.Map<CreateHashtagDTO>(page);
            return model;
        }

        public async Task<List<HashtagVM>> GetHashtags()
        {
            var hashtags = await _unitOfWork.HashtagRepository.GetFilteredList(
                selector: x => new HashtagVM
                {
                    Id = x.Id,
                    Post_Id = x.Post_Id,
                    Text = x.Text,

                },
                expression: x => x.Status != Status.Passive);
            return hashtags;
        }

        

        public async Task<bool> IsHashtagExsist(string text)
        {
            bool isExist = await _unitOfWork.HashtagRepository.Any(x => x.Text == text);
            return isExist;
        }
    }
}
