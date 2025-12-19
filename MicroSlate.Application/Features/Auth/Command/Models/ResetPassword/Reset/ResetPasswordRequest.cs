namespace MicroSlate.Application.Features.Auth.Command.Models.ResetPassword.Reset
{
    public class ResetPasswordRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
