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

namespace SMP.Application.Services.FollowService
{
    public class FollowService : IFollowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FollowService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task Create(CreateFollowerDTO model)
        {
            var follow = _mapper.Map<Follower>(model);
            await _unitOfWork.FollowerRepository.Create(follow);
            await _unitOfWork.Commit();
        }
        
        
        public async Task Delete(string id , string userId)
        {
            var follow = await _unitOfWork.FollowerRepository.GetDefault(X => X.FollowingId == id && X.FollowerId == userId);
            follow.Status = Status.Passive;
            follow.DeleteDate = DateTime.Now;
            await _unitOfWork.Commit();
        }

 

        public async Task<List<FollowVM>> GetFollowers(string id)
        {
            var followersList = await _unitOfWork.FollowerRepository.GetFilteredList(
                 selector: x=>  new FollowVM
                 {
                     Id = x.Id,
                     UserName = x.Following.UserName,
                     Image = x.Following.ImagePath,
                     User_Id = x.FollowingId,
                     User_Score = x.Following.User_Score,



                 },
                 expression: x => x.Status == Status.Active && x.FollowingId == id,
                 orderBy: x => x.OrderBy(y => y.CreateDate),
                 include: x => x.Include(x => x.Follow));

            return followersList;

        }


        public async Task<List<string>> PostFollowingControl(string id)
        {
            var followingList = await _unitOfWork.FollowerRepository.GetFilteredList(
                 selector: x => x.FollowingId,
                 expression: x => x.Status == Status.Active &&  x.FollowerId == id);

            return followingList;
        }
        //{

        //    var followings = await _unitOfWork.FollowerRepository.GetFilteredList(
        //        selector: x => x.FollowerId,
        //            expression: x => x.Status == Status.Active && x.FollowingId == id,
        //            orderBy: x => x.OrderBy(y => y.CreateDate),
        //            include: x => x.Include(x => x.Follow));

            
        //    return followings;
        //}




        public async Task<List<FollowVM>> GetFollowings(string id)
        {

            var followingList = await _unitOfWork.FollowerRepository.GetFilteredList(
               selector: x => new FollowVM
               {
                   Id = x.Id,
                   UserName = x.Following.UserName,
                   Image = x.Following.ImagePath,
                   User_Id = x.FollowingId,
                   User_Score = x.Following.User_Score,




               },
               expression: x => x.Status == Status.Active && x.Follow.Id == id,
               orderBy: x => x.OrderBy(y => y.CreateDate),
               include: x => x.Include(x => x.Follow));

            return followingList;
        }

        public async  Task<bool> IsFollowExsist(CreateFollowerDTO model)
        {
            bool isExist = await _unitOfWork.FollowerRepository.Any(x => x.Following.Id == model.FollowingId && x.Follow.Id == model.FollowerId && x.Status == Status.Active  );
            return isExist;
        }
    }
}
