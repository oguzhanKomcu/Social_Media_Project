using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

namespace SMP.Application.Services.PostSharingService
{
    public class PostSharingService : IPostSharingService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public PostSharingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task Create(PostSharingDTO model)
        {
            var postSharing = _mapper.Map<PostSharing>(model);
            await _unitOfWork.PostSharingRepository.Create(postSharing);
            await _unitOfWork.Commit();
        }
    

        public async Task Delete(int id)
        {
            
            var post = await _unitOfWork.PostSharingRepository.GetDefault(x => x.Id == id);
            post.Status = Status.Passive;
            post.DeleteDate = DateTime.Now;
            await _unitOfWork.Commit();
        }

        public async Task<List<PostandPostSharingVm>> RegisteredPostes()
        {
            var registeredPostes = await _unitOfWork.PostSharingRepository.GetFilteredList(
                selector: x => new PostandPostSharingVm
                {
                    Id = x.Id,
                    Post_Id = x.PostId,
                    User_Id = x.UserId,
                    Total_Score = x.Post.Total_Score.ToString(),
                    Total_Comment = x.Post.Total_Comment.ToString(),
                    ImagePath = x.Post.ImagePath,
                    Description = x.Post.Description,
                    UserName = x.User.UserName,
                    UserImagePath = x.User.ImagePath,
                    


                },
                expression: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(y => y.CreateDate),
               include: x => x.Include(y => y.Post).Include(y => y.User)
                );
            return registeredPostes;
        }



        public async Task<bool> IsRegisteredPostExsist(int postId)
        {

            bool isExist = await _unitOfWork.PostSharingRepository.Any(x => x.PostId == postId);
            return isExist;
        }

  
    }
}
