using MicroSlate.Service.DTOs.Auth;

namespace MicroSlate.Service.Abstracts.Auth
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDTO registerDto);
        Task<string> Login(string email, string password);
        Task<string> ConfirmEmail(int? userId, string? code);
        Task<string> SendResetPasswordCode(string Email);
        Task<string> ConfirmResetPassword(string Code, string Email);
        Task<string> ResetPassword(string Email, string Password);

    }
}
