namespace MicroSlate.Application.Features.Auth.Command.Models.ResetPassword.confirm
{
    public class ConfirmResetPasswordRequest
    {
        public string Code { get; set; }
        public string Email { get; set; }


    }
}
