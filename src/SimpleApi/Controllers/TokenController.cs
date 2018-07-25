using SimpleApi.Models;
using SimpleApi.Utilities;
using System.Web.Http;

namespace SimpleApi.Controllers
{
    public class TokenController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Get([FromBody]GetTokenRequest request)
        {
            if (CheckUser(request.Username, request.Password))
            {
                var token = JwtUtil.GenerateToken(request.Username);
                return Json(new { token });
            }
            return Unauthorized();
        }

        private bool CheckUser(string username, string password)
        {
            // should check in the database
            return true;
        }
    }
}