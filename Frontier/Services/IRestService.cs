using System.Net.Http;
using System.Threading.Tasks;

namespace Frontier.Services
{
    public interface IRestService
    {
        Task<HttpResponseMessage> PerformApiRequest(string url, HttpMethod method, object body = null, string emulate = null);
    }
}