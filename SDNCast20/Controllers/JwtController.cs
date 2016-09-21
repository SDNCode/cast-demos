using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System;
using ConsoleApplication.Infrastructure;

namespace ConsoleApplication.Controllers
{
    [Route("[Controller]")]
    public class JwtController : Controller
    {
        private readonly IConsumerValidator consumerValidator;
        private readonly IJwtProvider jwtProvider;
        public JwtController(IJwtProvider jwtProvider, IConsumerValidator consumerValidator)
        {
            this.jwtProvider = jwtProvider;
            this.consumerValidator = consumerValidator;
        }

        [Route("token")]
        public IActionResult Post()
        {
            var credentials = getCredentialsFromAuthorizationHeader(Request.Headers["Authorization"]);

            var valid = consumerValidator.Verify(credentials.Key, credentials.Value);
            if (valid)
            {
                var accessToken = jwtProvider.GenerateToken(credentials.Key);
                var result = new
                {
                    token_type = "Bearer",
                    access_token = accessToken
                };
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }

        private KeyValuePair<string, string> getCredentialsFromAuthorizationHeader(string headerValue)
        {
            var key = headerValue.Split(' ')[1];
            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(key)).Split(':');
            return new KeyValuePair<string, string>(decoded[0], decoded[1]);
        }
    }
}