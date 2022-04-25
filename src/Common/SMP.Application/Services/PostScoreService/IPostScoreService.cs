using SMP.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.PostScoreService
{
    public interface IPostScoreService
    {
        Task Create(PostScoreDTO model);

        
    }
}
