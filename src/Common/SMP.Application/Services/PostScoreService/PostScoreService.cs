using AutoMapper;
using SMP.Application.Models.DTOs;
using SMP.Domain.Enums;
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

            //var totalscore = await _unitOfWork.PostRepository.GetFilteredFirstOrDefault(
            //   selector: x => x.Post_Scores.Average(y => y.Score),
            //       expression: x => x.Status == Status.Active && x.Id == postScore.PostId);
            
            //await _unitOfWork.ExecuteSqlRaw("tp_Total_Score", parameteres: new { @postId = postScore.PostId, @postScore = totalscore });

        }

        public async Task<bool> IsScoreExsist(int postId, string id)
        {
            bool isExist = await _unitOfWork.PostScoreRepository.Any(x => x.UserId == id && x.PostId == postId);
            return isExist;
        }

    }
}
