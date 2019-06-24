using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CtrlBox.Domain.Identity;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using Microsoft.AspNetCore.Identity;

namespace CtrlBox.Application
{
   public class SecurityApplicationService: ISecurityApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SecurityApplicationService(IUnitOfWork unitOfWork, IMapper mapper, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IList<ApplicationUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }
    }
}
