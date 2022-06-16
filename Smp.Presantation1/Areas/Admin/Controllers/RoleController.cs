
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Smp.Presantation1.Areas.Admin.Models.DTOs;
using SMP.Domain.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Smp.Presantation1.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,
                              UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult List()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult Create() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Create([MinLength(3, ErrorMessage = "Minimum lenght is 3"),
                                                 Required(ErrorMessage = "Must to type role name")] string roleName)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (result.Succeeded)
                {
                    TempData["Success"] = "The role has been created..!";
                    return RedirectToAction("List");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        TempData["Error"] = $"{error.Description}";
                    }
                }
            }

            return RedirectToAction("Create");
        }


        public async Task<IActionResult> AssignedToUser(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            List<AppUser> hasRole = new List<AppUser>();
            List<AppUser> hasNoRole = new List<AppUser>();

            foreach (AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? hasRole : hasNoRole;
                list.Add(user);
            }

            AssignedRoleToUserDTO model = new AssignedRoleToUserDTO()
            {
                RoleName = role.Name,
                Role = role,
                HasRole = hasRole,
                HasNoRole = hasNoRole
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignedToUser(AssignedRoleToUserDTO model)
        {
            IdentityResult result;

            foreach (string userId in model.AddIds ?? new string[] { })
            {
                AppUser user = await _userManager.FindByIdAsync(userId);
                result = await _userManager.AddToRoleAsync(user, model.RoleName);
            }

            foreach (string userId in model.RemoveIds ?? new string[] { })
            {
                AppUser user = await _userManager.FindByIdAsync(userId);
                result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            }

            return RedirectToAction("List");
        }


    }
}
