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
        

        public async Task Delete(int id)
        {
            var follow = await _unitOfWork.FollowerRepository.GetDefault(X => X.Id == id);
            follow.Status = Status.Passive;
            follow.DeleteDate = DateTime.Now;
            await _unitOfWork.Commit();
        }

 

        public async Task<List<FollwersVm>> GetFollowers(int id)
        {
            var followersList = await _unitOfWork.FollowerRepository.GetFilteredList(
                 selector: x=>  new FollwersVm
                 {
                     Id = x.Id,
                     FollowerUserName = x.FollowingUser.UserName,
                     FollowerImage = x.FollowingUser.ImagePath,
                    

                 },
                 expression: x => x.Status == Status.Active && x.Following_UserId == id,
                 orderBy: x => x.OrderBy(y => y.CreateDate),
                 include: x => x.Include(x => x.FollowUser));

            return followersList;

        }
                 
                
           
        

        public  async Task<List<FollowingVM>> GetFollowings(int id)
        {

          var followingList = await _unitOfWork.FollowerRepository.GetFilteredList(
             selector: x => new FollowingVM
             {
                 Id = x.Id,
                 FollowUpUserName = x.FollowUser.UserName,
                 FollowUpImage = x.FollowUser.ImagePath,




             },
             expression: x => x.Status == Status.Active && x.Follow_User_Id == id,
             orderBy: x => x.OrderBy(y => y.CreateDate),
             include: x => x.Include(x => x.FollowUser));

           return followingList;
        }

        public async  Task<bool> IsFollowExsist(UpdateFollowerDTO model)
        {
            bool isExist = await _unitOfWork.FollowerRepository.Any(x => x.Following_UserId == model.Following_UserId && x.Follow_User_Id == model.Follow_User_Id);
            return isExist;
        }
    }
}
