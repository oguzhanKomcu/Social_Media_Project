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

 

        public async Task<List<FollwersVm>> GetFollowers(string id)
        {
            var followersList = await _unitOfWork.FollowerRepository.GetFilteredList(
                 selector: x=>  new FollwersVm
                 {
                     Id = x.Id,
                     FollowerUserName = x.Follow.UserName,
                     FollowerImage = x.Follow.ImagePath,
                     


                 },
                 expression: x => x.Status == Status.Active && x.FollowingId == id,
                 orderBy: x => x.OrderBy(y => y.CreateDate),
                 include: x => x.Include(x => x.Follow));

            return followersList;

        }


        public async Task<List<string>> PostFollowingControl(string id)
        {

            var followings = await _unitOfWork.FollowerRepository.GetFilteredList(
                selector: x => x.FollowerId,
                    expression: x => x.Status == Status.Active && x.FollowingId == id,
                    orderBy: x => x.OrderBy(y => y.CreateDate),
                    include: x => x.Include(x => x.Follow));

            
            return followings;
        }




        public async Task<List<FollowingVM>> GetFollowings(string id)
        {

            var followingList = await _unitOfWork.FollowerRepository.GetFilteredList(
               selector: x => new FollowingVM
               {
                   Id = x.Id,
                   FollowUpUserName = x.Follow.UserName,
                   FollowUpImage = x.Follow.ImagePath,




               },
               expression: x => x.Status == Status.Active && x.Follow.Id == id,
               orderBy: x => x.OrderBy(y => y.CreateDate),
               include: x => x.Include(x => x.Follow));

            return followingList;
        }

        public async  Task<bool> IsFollowExsist(CreateFollowerDTO model)
        {
            bool isExist = await _unitOfWork.FollowerRepository.Any(x => x.Following.Id == model.FollowingId && x.Follow.Id== model.FollowerId);
            return isExist;
        }
    }
}
