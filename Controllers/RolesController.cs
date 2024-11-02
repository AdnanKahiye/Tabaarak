using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tabaarak.Models;
using Tabaarak.Models.Entities;

namespace Tabaarak.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // Action to list all roles
        ///[Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        // Action to create a new role
        [HttpGet]

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to create role");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Role already exists");
                }
            }
            return View(roleName);
        }

        // New Action to create a user and assign a role
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var userCreateResult = await _userManager.CreateAsync(user, model.Password);

                if (userCreateResult.Succeeded)
                {
                    var roleAssignResult = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (roleAssignResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to assign role to user");
                    }
                }
                else
                {
                    foreach (var error in userCreateResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "All fields are required");
            }
            return View(model);
        }
    }
}
