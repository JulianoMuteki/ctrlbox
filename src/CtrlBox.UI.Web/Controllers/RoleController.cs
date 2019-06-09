using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CtrlBox.Domain.Security;
using CtrlBox.Infra.Context.Identity;
using CtrlBox.UI.Web.Helpers;
using CtrlBox.UI.Web.Models.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CtrlBox.UI.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult Create()
        {
            var role = new ApplicationRole();
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await _roleManager.RoleExistsAsync(role.Name);
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await _roleManager.CreateAsync(role);
            }

            return RedirectToAction("Index");
        }

        public IActionResult AssignRoleToUser()
        {
            RoleViewModel rolesToUsersViewModel = new RoleViewModel();

            rolesToUsersViewModel.AllRoles = _roleManager.Roles.ToList()
                                                   .Select(role => new SelectListItem
                                                   {
                                                       Value = role.Id.ToString(),
                                                       Text = role.Name
                                                   }).ToList();

            rolesToUsersViewModel.AllUsers = _userManager.Users.ToList()
                                                   .Select(user => new SelectListItem
                                                   {
                                                       Value = user.Id.ToString(),
                                                       Text = user.UserName
                                                   }).ToList();

            return View(rolesToUsersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(RoleViewModel rolesToUsersViewModel)
        {
            // ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(rolesToUsersViewModel.UserSelected);

                var result = await _userManager.AddToRoleAsync(user, rolesToUsersViewModel.RoleSelected);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                // AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetAjaxHandlerRoles(string userID)
        {
            var user = _userManager.FindByIdAsync(userID).Result;
            RoleViewModel rolesToUsersViewModel = new RoleViewModel();

            rolesToUsersViewModel.AllRoles = _roleManager.Roles.ToList()
                                                   .Select(role => new SelectListItem
                                                   {
                                                       Value = role.Id.ToString(),
                                                       Text = role.Name
                                                   }).ToList();

            var rolesSelectedNames = _userManager.GetRolesAsync(user).Result;

            rolesToUsersViewModel.AllRoles = rolesToUsersViewModel.AllRoles.Select(x => { x.Selected = rolesSelectedNames.Any(r => r.Equals(x.Text)); return x; }).ToList();
            return Json(rolesToUsersViewModel.AllRoles);
        }

        [HttpPost]
        public IActionResult PostAjaxHandlerRoles(string[] rolesJSON, string userID)
        {
            Guid id;

            if (Guid.TryParse(userID,out id))
            {
                var user = _userManager.Users.Where(u => u.Id.ToString() == userID).Include(x => x.UserRoles).FirstOrDefault();
                var rolesExists = user.UserRoles;

                JsonSerialize jsonS = new JsonSerialize();
                var rolesVM = jsonS.JsonDeserializeObject<List<DropDpwnViewModel>>(rolesJSON[0]);

                var rolesToAdd = rolesVM;

                if (rolesExists.Count > 0)
                    rolesToAdd = rolesVM.Where(x => rolesExists.Any(z => z.RoleId.ToString() != x.id)).ToList();

                var rolesUsersToRemove = rolesExists.Where(x => rolesVM.Any(z => z.id != x.RoleId.ToString())).ToList();
                var rolesToRemove = _roleManager.Roles.Where(x => rolesUsersToRemove.Any(y => y.RoleId == x.Id)).ToList();

                var result = _userManager.AddToRolesAsync(user, rolesToAdd.Select(x => x.text).ToArray()).Result;

                if (rolesUsersToRemove.Count > 0)
                    result = _userManager.RemoveFromRolesAsync(user, rolesToRemove.Select(x => x.Name).ToList()).Result;

                return Json(new { OK = "ok" });
            }
            else
            {
                return Json(new { OK = "Without user" });
            }
        }


        [HttpGet]
        public IActionResult GetAjaxHandlerClaimsRoles(string roleID)
        {
            RoleViewModel rolesViewModel = new RoleViewModel();

            var rol = _roleManager.FindByIdAsync(roleID).Result;
            var claims = _roleManager.GetClaimsAsync(rol).Result;

            var claimsListItem = PolicyTypes.IdentityClaims
                                                 .Select(claim => new SelectListItem
                                                 {
                                                     Value = claim.Value,
                                                     Text = claim.Value
                                                 }).ToList();

            rolesViewModel.AllClaims = claimsListItem.Select(x => { x.Selected = claims.Any(r => r.Value.Equals(x.Value)); return x; }).ToList();
            return Json(rolesViewModel.AllClaims);
        }


        [HttpGet]
        public IActionResult GetAjaxHandlerClaimsUsers(string userID)
        {
            RoleViewModel rolesViewModel = new RoleViewModel();

            var user = _userManager.FindByIdAsync(userID).Result;
            var claims = _userManager.GetClaimsAsync(user).Result;

            var claimsListItem = PolicyTypes.IdentityClaims
                                                 .Select(claim => new SelectListItem
                                                 {
                                                     Value = claim.Value,
                                                     Text = claim.Value
                                                 }).ToList();

            rolesViewModel.AllClaims = claimsListItem.Select(x => { x.Selected = claims.Any(r => r.Value.Equals(x.Value)); return x; }).ToList();
            return Json(rolesViewModel.AllClaims);
        }

        public IActionResult AssignClaimToUser()
        {
            RoleViewModel rolesToUsersViewModel = new RoleViewModel();

            rolesToUsersViewModel.AllRoles = _roleManager.Roles.ToList()
                                                   .Select(role => new SelectListItem
                                                   {
                                                       Value = role.Name,
                                                       Text = role.Name
                                                   }).ToList();

            return View(rolesToUsersViewModel);
        }

        public IActionResult AssignClaimToRole()
        {
            RoleViewModel rolesToUsersViewModel = new RoleViewModel();

            rolesToUsersViewModel.AllRoles = _roleManager.Roles.ToList()
                                                   .Select(role => new SelectListItem
                                                   {
                                                       Value = role.Name,
                                                       Text = role.Name
                                                   }).ToList();

            return View(rolesToUsersViewModel);
        }

        public IActionResult Layout()
        {
            RoleViewModel rolesToUsersViewModel = new RoleViewModel();

            rolesToUsersViewModel.AllRoles = _roleManager.Roles.ToList()
                                                   .Select(role => new SelectListItem
                                                   {
                                                       Value = role.Name,
                                                       Text = role.Name
                                                   }).ToList();

            return View(rolesToUsersViewModel);
        }
    }
}