using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SMP.Domain.Models.Entities;

namespace Smp.Presantation1.Areas.Admin.Models.Helpers
{
    [HtmlTargetElement("td", Attributes = "user-role")]
    public class RoleTagHelper : TagHelper
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleTagHelper(RoleManager<IdentityRole> roleManager,
                             UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HtmlAttributeName("user-role")]
        public string RoleId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> userNames = new List<string>();

            var role = await _roleManager.FindByIdAsync(RoleId);

            if (role != null)
            {
                foreach (var user in _userManager.Users)
                {
                    if (user != null && await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userNames.Add(user.UserName);
                    }
                }
            }

            output.Content.SetContent(userNames.Count == 0 ? "No User Assigned" : String.Join(",", userNames));
        }



    }
}
