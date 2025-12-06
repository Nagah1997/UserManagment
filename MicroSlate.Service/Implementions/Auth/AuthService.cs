using MicroSlate.Infrastructure.AppConct;
using MicroSlate.Infrastructure.Identity;
using MicroSlate.Service.Abstracts.Auth;
using MicroSlate.Service.Abstracts.Email;
using MicroSlate.Service.DTOs.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace MicroSlate.Service.Implementions.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDBContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenGenerator _jwt;
        private readonly IEmailsService _emailsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IUrlHelper _urlHelper;

        public AuthService(ApplicationDbContext applicationDBContext, IUrlHelperFactory urlHelperFactory,
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

        public async Task<string> Register(RegisterDTO registerDto)
        {
            var trans = await _applicationDBContext.Database.BeginTransactionAsync();
            try
            {
                var user = new ApplicationUser
                {
                    FullName = registerDto.FullName,
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };
                //if Email is Exist
                var existUser = await _userManager.FindByEmailAsync(user.Email);
                //email is Exist
                if (existUser != null) return "EmailIsExist";

                var userByUserName = await _userManager.FindByNameAsync(user.UserName);
                //username is Exist
                if (userByUserName != null) return "UserNameIsExist";

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded)
                    throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));

                // Default Role
                await _userManager.AddToRoleAsync(user, "User");


                //Send Confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);


                var requestAccessor = _httpContextAccessor.HttpContext.Request;


                var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

                var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";


                //message or body
                await _emailsService.SendEmail(user.Email, message, "ConFirm Email");

                var roles = await _userManager.GetRolesAsync(user);



                await trans.CommitAsync();
                return "Success";

            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }

        }

        public async Task<string> Login(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    throw new Exception("Invalid email or password");

                var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                if (!result.Succeeded)
                    throw new Exception("Invalid email or password");

                var roles = await _userManager.GetRolesAsync(user);

                return _jwt.GenerateToken(user, roles);
            }

            catch (Exception ex)
            {

                return $"{ex.Message} not login";



            }

        }
        public async Task<string> ConfirmEmail(int? userId, string? code)
        {
            if (userId == null || code == null)
                return "ErrorWhenConfirmEmail";
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirmEmail.Succeeded)
                return "ErrorWhenConfirmEmail";
            return "Success";
        }




        public async Task<string> SendResetPasswordCode(string Email)
        {
            var trans = await _applicationDBContext.Database.BeginTransactionAsync();
            try
            {
                //user
                var user = await _userManager.FindByEmailAsync(Email);
                //user not Exist => not found
                if (user == null)
                    return "UserNotFound";
                //Generate Random Number

                //Random generator = new Random();
                //string randomNumber = generator.Next(0, 1000000).ToString("D6");
                var chars = "0123456789";
                var random = new Random();
                var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

                //update User In Database Code
                user.Code = randomNumber;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                    return "ErrorInUpdateUser";
                var message = "Code To Reset Passsword : " + user.Code;
                //Send Code To  Email 
                await _emailsService.SendEmail(user.Email, message, "Reset Password");
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<string> ConfirmResetPassword(string Code, string Email)
        {
            //Get User
            //user
            var user = await _userManager.FindByEmailAsync(Email);
            //user not Exist => not found
            if (user == null)
                return "UserNotFound";
            //Decrept Code From Database User Code
            var userCode = user.Code;
            //Equal With Code
            if (userCode == Code) return "Success";
            return "Failed";
        }

        public async Task<string> ResetPassword(string Email, string Password)
        {
            var trans = await _applicationDBContext.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByEmailAsync(Email);
                //user not Exist => not found
                if (user == null)
                    return "UserNotFound";
                await _userManager.RemovePasswordAsync(user);
                if (!await _userManager.HasPasswordAsync(user))
                {
                    await _userManager.AddPasswordAsync(user, Password);
                }
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }


    }

}

