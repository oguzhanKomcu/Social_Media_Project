﻿using AutoMapper;
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

            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap();
            CreateMap<Post, GetPostVM>().ReverseMap();
            CreateMap<UpdatePostDTO, GetPostVM>().ReverseMap();

            CreateMap<Page, CreatePageDTO>().ReverseMap();
            CreateMap<Page, UpdatePageDTO>().ReverseMap();
            CreateMap<Page, GetPageVM>().ReverseMap();
            CreateMap<UpdatePageDTO, GetPageVM>().ReverseMap(); 


            CreateMap<Hashtag, CreateHashtagDTO>().ReverseMap();
            CreateMap<Hashtag, HashtagVM>().ReverseMap();


            
            CreateMap<FavoritePost, CreateFavoritePost>().ReverseMap();
            CreateMap<FavoritePost, FavoritePostVM>().ReverseMap();

            CreateMap<Follower, CreateFollowerDTO>().ReverseMap();
            CreateMap<Follower, FollwersVm>().ReverseMap();
            CreateMap<Follower, FollowingVM>().ReverseMap();
            
            CreateMap<Post_Score, PostScoreDTO>().ReverseMap();


           CreateMap<Post_Comment, CreatePostCommentDTO>().ReverseMap();
           CreateMap<Post_Comment, UpdatePostCommentDTO>().ReverseMap();
           CreateMap<Post_Comment, PostCommentVM>().ReverseMap();
           CreateMap<UpdatePostCommentDTO, PostCommentVM>().ReverseMap();

        }
    }
    
}