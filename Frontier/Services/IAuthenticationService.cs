using System.Threading.Tasks;

namespace Frontier.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> GetToken();
    }
}