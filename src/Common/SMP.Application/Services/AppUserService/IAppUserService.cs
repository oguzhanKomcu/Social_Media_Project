using Microsoft.AspNetCore.Identity;
using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using SMP.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.AppUserService
{
    public interface IAppUserService
    {
        Task<AppUser> Authentication(string userName, string password);
        Task<IdentityResult> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);
        Task LogOut();
        Task Delete(string id);
        Task UpdateUser(UpdateProfilDTO model);
        Task<GetAppUserVM> UserDetails(string userId, string followerId);
        Task<GetAppUserVM> UserProfile(string id);

        Task<UpdateProfilDTO> GetById(string id);
        Task<List<GetAppUserVM>> GetUsers();
        Task<List<GetAppUserVM>> GetUserName(string user);

    }
}
