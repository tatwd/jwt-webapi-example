using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleApi.Models
{
    public class GetTokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}