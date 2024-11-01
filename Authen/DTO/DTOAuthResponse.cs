using NuGet.Common;

namespace DNDServer.Authen.DTO
{
    public class DTOAuthResponse
    {
        public string? Token { get; set; }
        public bool? IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? RefreshToken {  get; set; }
    }
}
