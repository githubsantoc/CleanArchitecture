namespace Application.services
{
    public class JWTandRefreshService
    {
        public required string JwtToken { get; set; }
        public DateTime TokenExpiry { get; set; }

        public required string RefreshToken { get; set; }
    }
}
