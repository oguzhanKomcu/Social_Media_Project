using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.FavoritePostService
{
    public interface IFavoritePostService
    {
        Task Create(CreateFavoritePost model);
        Task Delete(int id);
        Task<CreateFavoritePost> GetById(int id);
        Task<List<FavoritePostVM>> GetFavoritePosts();
        Task<bool> IsFavoriteExsist(string text);
    }
}
