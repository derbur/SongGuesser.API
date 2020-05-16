using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SongGuesser.Models;
using Microsoft.Extensions.Configuration;

namespace SongGuesser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        // This logic could be pulled out into a service (if ever actually needed 🧐)
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private string APP_ID;
        private string APP_SECRET;
        private string REDIRECT_URI;

        public LoginController(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _config = config;
            APP_ID = _config["Providers:Deezer:AppId"];
            APP_SECRET = _config["Providers:Deezer:AppSecret"];
            REDIRECT_URI = _config["Providers:Deezer:RedirectUri"];
        }

        [HttpGet]
        public void Login()
        {
            Response.Redirect($"https://connect.deezer.com/oauth/auth.php?app_id={APP_ID}&redirect_uri={REDIRECT_URI}&perms=basic_access,email");
        }

        [Route("redirect")]
        public async Task<RedirectResult> HandleLoginResponse([FromQuery(Name = "code")] string accessCode, [FromQuery(Name = "error_reason")] string errorReason)
        {
            // TODO: Handle error cases
            if(!String.IsNullOrEmpty(accessCode))
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    $"https://connect.deezer.com/oauth/access_token.php?app_id={APP_ID}&secret={APP_SECRET}&code={accessCode}&output=json");

                var client = _clientFactory.CreateClient();

                var response = await client.SendAsync(request);

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonDocument.Parse(content);

                    var sgToken = new Token
                    {
                        Provider = Provider.Deezer,
                        Value = data.RootElement.GetProperty("access_token").GetString(),
                        Expiration = data.RootElement.GetProperty("expires").GetInt32()
                    };

                    SetSessionToken(sgToken);

                    return Redirect("~/");
                }
            }
            return Redirect("~/error");
        }

        private void SetSessionToken(Token token)
        {
            HttpContext.Session.SetString("_Token", token.Value);
            HttpContext.Session.SetInt32("_TokenExpiration", token.Expiration);
        }
    }
}