using e_library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace e_library.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ElearningDbContext _context;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            ElearningDbContext context) 
            : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization header was not found!");

            try
            {
                var authHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var bytes = Convert.FromBase64String(authHeaderValue.Parameter);
                string[] credntials = Encoding.UTF8.GetString(bytes).Split(":"); 
                string email = credntials[0];
                string password = credntials[1];

                User user = _context.Users.Where(user => user.Email == email && user.Password == password).FirstOrDefault();

                if (user != null)
                {
                    AuthenticateResult.Fail("Invalid Credentials!");
                } 
                else
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.Email) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
            }
            catch (Exception) 
            {
                return AuthenticateResult.Fail("Error has occured");
            }
            

            //throw new NotImplementedException();
            return AuthenticateResult.Fail("Need to implement");
        }
    }
}
