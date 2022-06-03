using AutoMapper;
using Microsoft.AspNet.Identity;
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


        public async Task Create(PostScoreDTO model, Post post, AppUser appUser)
        {

            var postScore = _mapper.Map<Post_Score>(model);

            await _unitOfWork.PostScoreRepository.Create(postScore);
            await _unitOfWork.Commit();

            var totalPostScore = await _unitOfWork.PostRepository.GetFilteredFirstOrDefault(
                  selector: x => new PostDTO
                  {
                      Id = x.Id,
                      Total_Score = x.Post_Scores.Average(y => y.Score),
                      Status = Status.Modified,
                      UpdateDate = DateTime.Now,
                      Description = x.Description,
                      User_Id = x.User_Id,
                      ImagePath = x.ImagePath,
                      CreateDate = x.CreateDate,
                      DeleteDate = x.DeleteDate,
                      Total_Comment = x.Total_Comment,


                  },
                  expression: x => x.Id == postScore.PostId && x.Status != Status.Passive);

            var postUpdate = _mapper.Map<Post>(totalPostScore);
            _unitOfWork.PostRepository.Update(postUpdate);
            await _unitOfWork.Commit();


            await _unitOfWork.Commit();


        }

        public async Task<bool> IsScoreExsist(int postId, string id)
        {
            bool isExist = await _unitOfWork.PostScoreRepository.Any(x => x.UserId == id && x.PostId == postId);
            return isExist;
        }

    }
}
