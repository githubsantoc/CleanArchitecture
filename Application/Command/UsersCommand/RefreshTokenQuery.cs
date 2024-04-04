using MediatR;

namespace CQRSApplication.UserInfo.Query
{
    public class RefreshTokenQuery : IRequest<string> 
    {
        public string? RefreshToken { get; set; }
    }
}
