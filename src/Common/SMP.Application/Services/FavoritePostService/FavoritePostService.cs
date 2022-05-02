﻿using AutoMapper;
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

namespace SMP.Application.Services.FavoritePostService
{
    public class FavoritePostService : IFavoritePostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FavoritePostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task Create(CreateFavoritePost model)
        {
            var favoritePost = _mapper.Map<FavoritePost>(model);
            await _unitOfWork.FavoritePostRepository.Create(favoritePost);
            await _unitOfWork.Commit();

        }

        public async Task Delete(int id)
        {

            var favoritePost = await _unitOfWork.FavoritePostRepository.GetDefault(x => x.Id == id);
            favoritePost.Status = Status.Passive;
            favoritePost.DeleteDate = DateTime.Now;
            await _unitOfWork.Commit();
        }

        public async Task<CreateFavoritePost> GetById(int id)
        {
            var favoritePost = await _unitOfWork.FavoritePostRepository.GetFilteredFirstOrDefault(
                selector: x=> new FavoritePostVM
                {
                    Id = x.Id,
                    User_Id = x.UserId,
                    Post_Id = x.PostId,
                    
                },
                expression: x => x.Id == id && x.Status == Status.Active);

            var mode = _mapper.Map<CreateFavoritePost>(favoritePost);
            return mode;
        }
                

               
        

        public async Task<List<FavoritePostVM>> GetFavoritePosts()
        {
            var favoritePosts = await _unitOfWork.FavoritePostRepository.GetFilteredList(
                selector: x => new FavoritePostVM
                {
                    Id = x.Id,
                    User_Id = x.UserId,
                    Post_Id = x.PostId,


                },
                expression: x => x.Status == Status.Active,
                orderBy: x => x.OrderBy(x => x.CreateDate),
                include: x => x.Include(x => x.Post).Include(x => x.User));

            return favoritePosts;


        }

        public async Task<bool> IsFavoriteExsist(string text)
        {
            bool isExist = await _unitOfWork.HashtagRepository.Any(x => x.Text == text);
            return isExist;
        }
    }
}