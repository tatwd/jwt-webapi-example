using SimpleApi.Filters;
using System.Web.Http;

namespace SimpleApi.Controllers
{
    public class ValueController : ApiController
    {
        [JwtAuthentication]
        public string Get()
        {
            return "value";
        }
    }
}