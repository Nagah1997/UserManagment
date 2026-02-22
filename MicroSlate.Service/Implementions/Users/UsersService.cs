using MicroSlate.Infrastructure.AppConct;
using MicroSlate.Infrastructure.Identity;
using MicroSlate.Service.Abstracts.Auth;
using MicroSlate.Service.Abstracts.Email;
using MicroSlate.Service.Abstracts.Users;
using MicroSlate.Service.DTOs.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroSlate.Service.Implementions.Users
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDBContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenGenerator _jwt;
        private readonly IEmailsService _emailsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IUrlHelper _urlHelper;

        public UsersService(ApplicationDbContext applicationDBContext, IUrlHelperFactory urlHelperFactory,
             IEmailsService emailsService, IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,

            SignInManager<ApplicationUser> signInManager,
            IJwtTokenGenerator jwt)



        {
            var httpContext = httpContextAccessor.HttpContext;
            _urlHelper = urlHelperFactory.GetUrlHelper(
                new ActionContext(httpContext, httpContext.GetRouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()));


            _emailsService = emailsService;
            _applicationDBContext = applicationDBContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;

            _jwt = jwt;
        }

        public async Task<List<GetAllUserDTO>> GetAllUsers()
        {
            
           var users = _userManager.Users.ToList();
            var usersDTO = new List<GetAllUserDTO>();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                usersDTO.Add(new GetAllUserDTO
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = userRoles.ToList()
                });
            }
            return  usersDTO;
        }
    }
}
