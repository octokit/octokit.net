using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nocto.Http
{
    public class HttpClientAdapter : IApplication
    {
        public async Task<IApplication> Invoke<T>(Environment<T> environment)
        {
            var http = new HttpClient { BaseAddress = environment.Request.BaseAddress };

            var request = new HttpRequestMessage(environment.Request.Method, environment.Request.Endpoint);
            foreach (var header in environment.Request.Headers)
                request.Headers.Add(header.Key, header.Value);

            var body = environment.Request.Body as string;
            if (body != null)
            {
                request.Content = new StringContent(body, Encoding.UTF8);
            }

            // Make the request
            var res = await http.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            environment.Response.Body = await res.EnsureSuccessStatusCode()
                .Content.ReadAsStringAsync();

            foreach (var h in res.Headers)
                environment.Response.Headers.Add(h.Key, h.Value.First());

            return this;
        }
    }
}
