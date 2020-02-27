using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TestMVCApi.Models;

namespace TestMVCApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<JObject>> Login(LoginRequest loginRequest)
        {
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");

            if (disco.IsError)
            {
                return Unauthorized();
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = loginRequest.ClientId,
                ClientSecret = loginRequest.ClientSecret,
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                return Unauthorized();
            }

            return tokenResponse.Json;
        }
    }
}