using AutoMapper;
using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using SMP.Domain.Models.Entities;
using SMP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.PostService
{
    public class PostService : IPostService 
    {

        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public async Task Create(CreatePostDTO model)
        {
            var post = _mapper.Map<Post>(model);


            
            return _postRepository.Create(post);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UpdatePostDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetPostVM>> GetPosts()
        {
            throw new NotImplementedException();
        }

        public Task<List<GetPostVM>> GetPostsForMembers()
        {
            throw new NotImplementedException();
        }

        public Task Update(UpdatePostDTO model)
        {
            throw new NotImplementedException();
        }
    }
