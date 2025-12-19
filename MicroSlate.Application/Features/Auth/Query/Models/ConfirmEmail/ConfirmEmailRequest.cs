namespace MicroSlate.Application.Features.Auth.Query.Models.ConfirmEmail
{
    public class ConfirmEmailRequest
    {
        public int UserId { get; set; }
        public string Code { get; set; }
    }
}
