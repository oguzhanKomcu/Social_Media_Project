using AutoMapper;
using SMP.Application.Models.DTOs;
using SMP.Domain.Models.Entities;
using SMP.Domain.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.PostScoreService
{
    public class PostScoreService : IPostScoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public PostScoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(PostScoreDTO model)
        {

            var postScore = _mapper.Map<Post_Score>(model);
            await _unitOfWork.PostScoreRepository.Create(postScore);
            await _unitOfWork.Commit();
           
        }
    }
}
