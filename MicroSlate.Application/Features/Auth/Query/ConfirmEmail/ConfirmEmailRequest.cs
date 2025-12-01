namespace MicroSlate.Application.Features.Auth.Query.ConfirmEmail
{
    public class ConfirmEmailRequest
    {
        public int UserId { get; set; }
        public string Code { get; set; }
    }
}
