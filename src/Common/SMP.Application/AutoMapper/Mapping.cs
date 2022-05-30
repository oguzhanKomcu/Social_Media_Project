using AutoMapper;
using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {

            CreateMap<Post, PostDTO>().ReverseMap();

            CreateMap<Post, GetPostVM>().ReverseMap();
            CreateMap<PostDTO, GetPostVM>().ReverseMap();
            CreateMap<PostDTO, PostDetailsVM>().ReverseMap();

            CreateMap<Page, CreatePageDTO>().ReverseMap();
            CreateMap<Page, UpdatePageDTO>().ReverseMap();
            CreateMap<Page, GetPageVM>().ReverseMap();
            CreateMap<UpdatePageDTO, GetPageVM>().ReverseMap(); 


            CreateMap<Hashtag, CreateHashtagDTO>().ReverseMap();
            CreateMap<Hashtag, HashtagVM>().ReverseMap();


            
            CreateMap<FavoritePost, CreateFavoritePost>().ReverseMap();
            CreateMap<FavoritePost, FavoritePostVM>().ReverseMap();

            CreateMap<PostSharing, PostSharingDTO>().ReverseMap();
            CreateMap<PostSharing, PostandPostSharingVm>().ReverseMap();
            

            CreateMap<Follower, CreateFollowerDTO>().ReverseMap();
            CreateMap<Follower, FollwersVm>().ReverseMap();
            CreateMap<Follower, FollowingVM>().ReverseMap();
            
            CreateMap<PostScoreDTO, Post_Score>().ReverseMap();


           CreateMap<Post_Comment, CreatePostCommentDTO>().ReverseMap();
           CreateMap<Post_Comment, UpdatePostCommentDTO>().ReverseMap();
           CreateMap<Post_Comment, PostCommentVM>().ReverseMap();
           CreateMap<UpdatePostCommentDTO, PostCommentVM>().ReverseMap();


            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, GetAppUserVM>().ReverseMap();
            CreateMap<UpdateProfilDTO, GetAppUserVM>().ReverseMap();
        }
    }
    
}
