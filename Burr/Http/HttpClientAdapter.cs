using System.Net.Http;
using System.Threading.Tasks;

namespace Burr.Http
{
    public class HttpClientAdapter : IApplication
    {
        public async Task<IApplication> Call<T>(Env<T> env)
        {
            var http = new HttpClient { BaseAddress = env.Request.BaseAddress };

            env.Response.Body = await http.GetStringAsync(env.Request.Endpoint);

            return this;
        }
    }
}
