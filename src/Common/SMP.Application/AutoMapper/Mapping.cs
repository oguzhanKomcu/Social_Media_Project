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

            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap();
            CreateMap<Post, GetPostVM>().ReverseMap();
            CreateMap<UpdatePostDTO, GetPostVM>().ReverseMap();
        }
    }
    
}
