using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.PostCommentService
{
    public  interface IPostCommentService
    {

        Task Create(CreatePostCommentDTO model);
        Task Delete(int id);
        Task Update(UpdatePostCommentDTO model);
        Task<List<PostCommentVM>> GetPostComments(int id);


    }
}
