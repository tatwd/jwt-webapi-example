using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SimpleApi.Filters
{
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public string ReasonPhrase { get; set; }
        public HttpRequestMessage Request { get; set; }

        public AuthenticationFailureResult(string resonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = resonPhrase;
            Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = Request,
                ReasonPhrase = ReasonPhrase
            };
            return response;
        }
    }
}