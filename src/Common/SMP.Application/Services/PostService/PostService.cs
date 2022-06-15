using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using SMP.Application.Services.FollowService;
using SMP.Application.Services.HashtagService;
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
        private readonly IFollowService _followService;
        private readonly IHashtagService _hashtagService; 


        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, IFollowService followService, IHashtagService hashtagService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _followService = followService;
            _hashtagService = hashtagService;
        }

        public async Task Create(PostDTO model)
        {
            model.CreateDate =  DateTime.Now;
            model.Status = Status.Active;
            var post = _mapper.Map<Post>(model);

            if (model.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(256, 256));
                string guid = Guid.NewGuid().ToString();
                image.Save($"wwwroot/images/posts/{guid}.jpg");
                post.ImagePath = $"/images/posts/{guid}.jpg";


            }

            await _unitOfWork.PostRepository.Create(post);
            




            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var post = await _unitOfWork.PostRepository.GetDefault(x => x.Id == id);
            post.Status = Status.Passive;
            post.DeleteDate = DateTime.Now;
            await _unitOfWork.Commit();
        }

        public async Task<PostDTO> GetById(int id)
        {
            var post = await _unitOfWork.PostRepository.GetFilteredFirstOrDefault(
                selector: x => new PostDTO
                {
                    Id = x.Id,

                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    CreateDate = x.CreateDate,
                    User_Id = x.User_Id,
                    //UserName = x.AppUser.UserName,
                    //UserImagePath = x.AppUser.ImagePath,
                    //Total_Score = x.Post_Scores.Average(y => y.Score).ToString(),
                    //Total_Comment = x.Post_Comments.Count(y => y.Id == id).ToString(),


                },
                expression: x => x.Id == id && x.Status != Status.Passive
                );

            //var model = _mapper.Map<PostDTO>(post);
       


            return post;
        }

        public async Task<List<GetPostVM>> UserGetPosts(string id)
            
        {


            var posts = await _unitOfWork.PostRepository.GetFilteredList(
                selector: x => new GetPostVM
                {
                    Id = x.Id,
                    User_Id = x.User_Id,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    UserName = x.AppUser.UserName,
                    UserImagePath = x.AppUser.ImagePath,
                    Total_Score = x.Post_Scores.Average(y => y.Score).ToString(),
                    Total_Comment = x.Post_Comments.Count(y => y.Id == x.Id).ToString(),
                    CreateDate = x.CreateDate,
                    


                },
                expression: x => x.User_Id == id   && x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.CreateDate));

            return posts;
        }
        
        public async Task<List<GetPostVM>> GetPostsForMembers(string id)
        {
            var followings = await _followService.PostFollowingControl(id);
            

            var posts = await _unitOfWork.PostRepository.GetFilteredList(
                selector: x => new GetPostVM
                {
                    Id = x.Id,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    UserName = x.AppUser.UserName,
                    UserImagePath = x.AppUser.ImagePath,
                    Total_Score = x.Post_Scores.Average(y => y.Score).ToString(),
                    Total_Comment = x.Post_Comments.Count(y => y.Id == x.Id).ToString(),
                    User_Id = x.AppUser.Id,
                    CreateDate = x.CreateDate,

                },
           
               expression: x => x.Status != Status.Passive && followings.Contains(x.User_Id),
                orderBy: x => x.OrderByDescending(x => x.CreateDate),
              include: x => x.Include(x => x.AppUser).Include(x => x.Post_Comments).Include(x => x.Post_Scores));


            

            return posts;

        }



        public async Task Update(PostDTO model)
        {
            model.UpdateDate = DateTime.Now;
            model.Status = Status.Modified;
            var post = _mapper.Map<Post>(model);

            if (model.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(256, 256));
                string guid = Guid.NewGuid().ToString();
                image.Save($"wwwroot/images/posts/{guid}.jpg");
                post.ImagePath = $"/images/posts/{guid}.jpg";


            }

             _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.Commit();
        }

        public async Task<List<GetPostVM>> GetAllPostsUserName(string userName)
        {


            var users = await _unitOfWork.PostRepository.GetFilteredList(
               selector: x => new GetPostVM
               {
                   Id = x.Id,
                   UserName = x.AppUser.UserName,
                   ImagePath = x.ImagePath,
                   Total_Score = x.Total_Score.ToString(),
               },
               expression: x => x.Status != Domain.Enums.Status.Passive && x.AppUser.UserName.Contains(userName) || userName == null);


            return users;
        }

        public async Task<PostDetailsVM> GetPostDetails(int id)
        {
            var post = await _unitOfWork.PostRepository.GetFilteredFirstOrDefault(
                selector: x => new PostDetailsVM
                {
                    Id = x.Id,

                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    UserName = x.AppUser.UserName,
                    UserImagePath = x.AppUser.ImagePath,
                    User_Id = x.User_Id,
                    Total_Score = x.Post_Scores.Average(y => y.Score) != null
                    ? 
                    Math.Round(x.Post_Scores.Average(y => y.Score), 1).ToString().Remove(4)
                    : 
                    Math.Round(x.Post_Scores.Average(y => y.Score), 1).ToString(),

                    Total_Comment = x.Post_Comments.Count(y => y.PostId == id).ToString(),
                    CreateDate = x.CreateDate,
                    Comments = x.Post_Comments.Where(x => x.PostId == id && x.Status != Status.Passive)
                    .OrderByDescending(x => x.CreateDate)
                    .Select(x => new PostCommentVM
                    {
                        Id = x.Id,
                        User_Id = x.UserId,
                        Text = x.Text,
                        UserImage = x.User.ImagePath,
                        UserName = x.User.UserName,
                        CreateDate = x.CreateDate.ToString()
                    }).ToList(),


                },
                expression: x => x.Id == id && x.Status != Status.Passive,
                include: x => x.Include(x => x.AppUser).Include(x => x.Post_Comments).Include(x => x.Post_Scores));
                

            var model = _mapper.Map<PostDTO>(post);
            model.Post_Comments = await _unitOfWork.PostCommentRepository.GetFilteredList(
                selector: x => new PostCommentVM
                {
                    Id = x.Id,
                    Post_Id = x.PostId,
                    User_Id = x.UserId,
                    Text = x.Text,
                },
                expression: x => x.PostId == id);


            model.Post_Scores = await _unitOfWork.PostScoreRepository.GetFilteredList(
                selector: x => new PostScoreVM
                {
                    Id = x.Id,
                    Post_Id = x.PostId,
                    User_Id = x.UserId,
                    Score = x.Score,

                },
            expression: x => x.PostId == id);




            return post;
        }

    }
}
