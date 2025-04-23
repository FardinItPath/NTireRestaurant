using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using R.DAL.Context;
using R.DAL.EntityModel;
using Common.Helper;
using Microsoft.AspNetCore.Identity.Data;

namespace NTireRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly RestaurantDbContext _context;
        private readonly IConfiguration _config;
        private readonly JWTService _jwtService;

        public AccountController(RestaurantDbContext context, IConfiguration config, JWTService jwtService)
        {
            _context = context;
            _config = config;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
                return BadRequest("Username already exists.");

            user.CreatedDT = DateTime.UtcNow;
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Registration successful.");
        }

      
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel user)
        {
            var dbUser = _context.Users.FirstOrDefault(u =>
                u.Username == user.Username && u.Password == user.Password);

            if (dbUser == null)
                return Unauthorized("Invalid username or password.");

            //var token = GenerateJwtToken(dbUser);
               var token = _jwtService.GenerateToken(dbUser.Username, dbUser.RoleId.ToString());
            return Ok(new { Token = token, Username = dbUser.Username });
        }
        //[HttpPost("forgot-password")]
        //public IActionResult ForgotPassword([FromBody] ForgotPasswordRequest request)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

        //    if (user == null)
        //        return NotFound("Username not found.");

        //    // You can optionally generate a reset token or return a status
        //    return Ok("Username verified. Proceed to reset your password.");
        //}

        //[HttpPost("reset-password")]
        //public IActionResult ResetPassword([FromBody] ResetPasswordRequest request)
        //{
        //    if (request.NewPassword != request.ConfirmPassword)
        //        return BadRequest("Passwords do not match.");

        //    var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

        //    if (user == null)
        //        return NotFound("User not found.");

        //    user.Password = request.NewPassword;
        //    user.UpdatedDT = DateTime.UtcNow;

        //    _context.SaveChanges();

        //    return Ok("Password has been successfully reset.");
        //}

        //private string GenerateJwtToken(UserModel user)
        //{
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.Name, user.Username!),
        //        new Claim(ClaimTypes.Role, user.RoleId.ToString())
        //    };

        //    var token = new JwtSecurityToken(
        //        issuer: _config["Jwt:Issuer"],
        //        audience: _config["Jwt:Audience"],
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddHours(1),
        //        signingCredentials: creds);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}

