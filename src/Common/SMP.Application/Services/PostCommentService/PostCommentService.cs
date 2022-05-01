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

namespace SMP.Application.Services.PostCommentService
{
    public class PostCommentService : IPostCommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public PostCommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Create(CreatePostCommentDTO model)
        {
            var postComment = _mapper.Map<Post_Comment>(model);
            await _unitOfWork.PostCommentRepository.Create(postComment);
            await _unitOfWork.Commit();
        }

        public async  Task Delete(int id)
        {
            var category = await _unitOfWork.PostCommentRepository.GetDefault(x => x.Id == id);
            category.Status = Status.Passive;
            category.DeleteDate = DateTime.Now;
            await _unitOfWork.Commit();
        }

        public async Task<List<PostCommentVM>> GetPostComments(int id)
        {
            var categories = await _unitOfWork.PostCommentRepository.GetFilteredList(
                selector: x => new PostCommentVM
                {
                    Id = x.Id,
                    
                    User_Id = x.UserId,
                    UserName = x.User.UserName,
                    Text = x.Text,
                },
                expression: x => x.Status != Status.Passive && x.PostId == id,   
        orderBy: x => x.OrderBy(x => x.CreateDate));

            return categories;
        }

        public async Task Update(UpdatePostCommentDTO model)
        {
            var category = _mapper.Map<Post_Comment>(model);
            _unitOfWork.PostCommentRepository.Update(category);
            await _unitOfWork.Commit();
        }
    }
}
