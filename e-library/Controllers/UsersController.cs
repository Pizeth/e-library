using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_library.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace e_library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ElearningDbContext _context;
        private readonly JWTSetting _jwtSetting;
        private readonly IServer _server;

        public UsersController(ElearningDbContext context, IOptions<JWTSetting> jwtsetting, IServer server)
        {
            _context = context;
            _jwtSetting = jwtsetting.Value;
            _server = server;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/5
        [HttpGet("GetUserDetails/{id}")]
        public async Task<ActionResult<User>> GetUserDetails(int id)
        {
            var user = await _context.Users.Include(u => u.Role)
                                            .Where(u => u.Id == id)
                                            .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //GET: api/User
        [HttpPost("Login")]
        public async Task<ActionResult<UserWithToken>> Login([FromBody] User user)
        {
            user = await _context.Users.Include(u => u.Role)
                                        .Where(u => (u.Email == user.Username
                                                || u.Username == user.Username)
                                                && u.Password == user.Password).FirstOrDefaultAsync();
            UserWithToken userWithToken;
            if (user != null)
            {
                userWithToken = new UserWithToken(user);
                RefreshToken refreshToken = GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync();

                userWithToken = new UserWithToken(user);
                userWithToken.RefreshToken = refreshToken.Token;
                //sign your token here here..
                userWithToken.AccessToken = GenerateAccessToken(user.Id);
                return userWithToken;
            } else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationtoken)
        {
            var result = await WriteFile(file);
            return Ok(result);
        }

        [HttpGet]
        [Route("Path")]
        public async Task<string> ToPath()
        {
            var addresses = GetPath().Result;
            return addresses;
        }

        private async Task<string> GetPath()
        {
            //string filename = "";
            //var addresses = string.Join(", ", _server.Features.Get<IServerAddressesFeature>().Addresses.ToArray());
            string[] addresses = _server.Features.Get<IServerAddressesFeature>().Addresses.ToArray();
            return addresses[0];
        }


        // POST: api/Users
        [HttpPost("Register")]
        public async Task<ActionResult<UserWithToken>> RegisterUser(IFormFile file, [FromBody] User user, CancellationToken cancellationtoken)
        {
            var result = await WriteFile(file);
            user.Avatar = result;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //load role for registered user
            user = await _context.Users.Include(u => u.Role)
                                        .Where(u => u.Id == user.Id).FirstOrDefaultAsync();

            UserWithToken userWithToken = null;

            if (user != null)
            {
                RefreshToken refreshToken = GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync();

                userWithToken = new UserWithToken(user);
                userWithToken.RefreshToken = refreshToken.Token;
            }

            if (userWithToken == null)
            {
                return NotFound();
            }

            //sign your token here here..
            userWithToken.AccessToken = GenerateAccessToken(user.Id);
            return userWithToken;
        }

        private async Task<string> WriteFile(IFormFile file)
        {
            //string filename = "";
            string exactpath = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var filename = DateTime.Now.Ticks.ToString() + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
            }
            return exactpath;
        }

        // GET: api/Users
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<UserWithToken>?> RefreshToken([FromBody] RefreshRequest refreshRequest)
        {
            User user = await GetUserFromAccessToken(refreshRequest.AccessToken);

            if (user != null && ValidateRefreshToken(user, refreshRequest.RefreshToken))
            {
                UserWithToken userWithToken = new UserWithToken(user);
                userWithToken.AccessToken = GenerateAccessToken(user.Id);

                return userWithToken;
            }

            return null;
        }
        // GET: api/Users
        [HttpPost("GetUserByAccessToken")]
        public async Task<ActionResult<User>?> GetUserByAccessToken([FromBody] string accessToken)
        {
            User user = await GetUserFromAccessToken(accessToken);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        private bool ValidateRefreshToken(User user, string refreshToken)
        {

            RefreshToken refreshTokenUser = _context.RefreshTokens.Where(rt => rt.Token == refreshToken)
                                                .OrderByDescending(rt => rt.ExpiryDate)
                                                .FirstOrDefault();

            if (refreshTokenUser != null && refreshTokenUser.UserId == user.Id
                && refreshTokenUser.ExpiryDate > DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }

        private async Task<User> GetUserFromAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSetting.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                //JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
                //if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))

                if (securityToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = principle.FindFirst(ClaimTypes.Name)?.Value;

                    return await _context.Users.Include(u => u.Role)
                                        .Where(u => u.Id == Convert.ToInt32(userId)).FirstOrDefaultAsync();
                }
            }
            catch (Exception)
            {
                return new User();
            }

            return new User();
        }

        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths(6);

            return refreshToken;
        }

        private string GenerateAccessToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSetting.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'ElearningDbContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
