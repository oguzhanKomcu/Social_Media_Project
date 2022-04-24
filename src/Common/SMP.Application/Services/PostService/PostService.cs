using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using SMP.Domain.Enums;
using SMP.Domain.Models.Entities;
using SMP.Domain.Repositories;
using SMP.Domain.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.PostService
{
    public class PostService : IPostService
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(CreatePostDTO model)
        {
            var product = _mapper.Map<Post>(model);

            if (model.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(256, 256));
                string guid = Guid.NewGuid().ToString();
                image.Save($"wwwroot/images/products/{guid}.jpg");
                product.ImagePath = $"/images/products/{guid}.jpg";


            }

            await _unitOfWork.PostRepository.Create(product);
            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var post = await _unitOfWork.PostRepository.GetDefault(x => x.Id == id);
            post.Status = Status.Passive;
            post.DeleteDate = DateTime.Now;
            await _unitOfWork.Commit();
        }

        public async Task<UpdatePostDTO> GetById(int id)
        {
            var post = await _unitOfWork.PostRepository.GetFilteredFirstOrDefault(
                selector: x => new GetPostVM
                {
                    Id = x.Id,

                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    UserName = x.AppUser.UserName,
                    UserImagePath = x.AppUser.ImagePath,
                    Total_Score = x.Post_Scores.Average(y => y.Score),
                    Total_Comment = x.Post_Comments.Count(y => y.Id == id),

                },
                expression: x => x.Id == id && x.Status == Status.Active
                );

            var model = _mapper.Map<UpdatePostDTO>(post);
            model.Post_Comments = await _unitOfWork.PostCommentRepository.GetFilteredList(
                selector: x => new PostCommentVM
                {
                    Id = x.Id,
                    Post_Id = x.Post_Id,
                    User_Id = x.User_Id,
                    Text = x.Text,
                },
                expression: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.CreateDate));

            model.Post_Scores = await _unitOfWork.PostScoreRepository.GetFilteredList(
                selector: x=> new PostScoreVM
                {
                    Id = x.Id,
                    Post_Id = x.Post_Id,
                    User_Id = x.User_Id,
                    Score = x.Score,

                },
                expression: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.Score));
              
            

            return model;
        }

        public async Task<List<GetPostVM>> GetPosts()
        {
            var posts = await _unitOfWork.PostRepository.GetFilteredList(
                selector: x => new GetPostVM
                {
                    Id = x.Id,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    UserName = x.AppUser.UserName,
                    UserImagePath = x.AppUser.ImagePath,
                    Total_Score = x.Post_Scores.Average(y => y.Score),
                    Total_Comment = x.Post_Comments.Count(y => y.Id == x.Id),
                    CreateDate = x.CreateDate,



                },
                expression: x => x.Status == Status.Active,
                orderBy: x => x.OrderBy(y => y.CreateDate),
                include: x => x.Include(x => x.AppUser).Include(x => x.Post_Comments).Include(x => x.Post_Scores)
                );

            return posts;
        }

        public async Task<List<GetPostVM>> GetPostsForMembers()
        {
            var posts = await _unitOfWork.PostRepository.GetFilteredList(
                selector: x => new GetPostVM
                {
                    Id = x.Id,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    UserName = x.AppUser.UserName,
                    UserImagePath = x.AppUser.ImagePath,
                    Total_Score = x.Post_Scores.Average(y => y.Score),
                    Total_Comment = x.Post_Comments.Count(y => y.Id == x.Id),
                    User_Id = x.AppUser.Id,
                    CreateDate = x.CreateDate,

                },
                expression: x => x.Status == Status.Active,
                orderBy: x => x.OrderByDescending(x => x.CreateDate),
                include: x => x.Include(x => x.AppUser).Include(x => x.Post_Comments).Include(x => x.Post_Scores));

            return posts;

        }



        public async Task Update(UpdatePostDTO model)
        {

            var product = _mapper.Map<Post>(model);

            if (model.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(256, 256));
                string guid = Guid.NewGuid().ToString();
                image.Save($"wwwroot/images/products/{guid}.jpg");
                product.ImagePath = $"/images/products/{guid}.jpg";


            }

            await _unitOfWork.PostRepository.Create(product);
            await _unitOfWork.Commit();
        }
    }
}
