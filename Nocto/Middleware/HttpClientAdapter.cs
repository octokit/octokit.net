using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nocto.Http
{
    public class HttpClientAdapter : IApplication
    {
        public async Task<IApplication> Call<T>(Env<T> env)
        {
            var http = new HttpClient { BaseAddress = env.Request.BaseAddress };

            var request = new HttpRequestMessage(new HttpMethod(env.Request.Method), env.Request.Endpoint);
            foreach (var header in env.Request.Headers)
                request.Headers.Add(header.Key, header.Value);

            var body = env.Request.Body as string;
            if (body != null)
            {
                request.Content = new StringContent(body, Encoding.UTF8);
            }

            // Make the request
            var res = await http.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            env.Response.Body = await res.EnsureSuccessStatusCode()
                .Content.ReadAsStringAsync();

            foreach (var h in res.Headers)
                env.Response.Headers.Add(h.Key, h.Value.First());

            return this;
        }
    }
}
