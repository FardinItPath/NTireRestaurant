using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using R.DAL.Context;
using R.DAL.EntityModel;
using Common.ViewModel;
using Common.Helper;

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
        public IActionResult Register([FromBody] UserViewModel userViewModel)
        {
            if (_context.Users.Any(u => u.Username == userViewModel.Username))
                return BadRequest("Username already exists.");

            var user = new UserModel
            {
                Username = userViewModel.Username,
                Password = PasswordHasher.HashPassword(userViewModel.Password),
                RoleId = userViewModel.RoleId,
                CreatedDT = DateTime.UtcNow
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Registration successful.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserViewModel userViewModel)
        {
            var hashedPassword = PasswordHasher.HashPassword(userViewModel.Password);
            var dbUser = _context.Users.FirstOrDefault(u =>
                u.Username == userViewModel.Username && u.Password == hashedPassword);

            if (dbUser == null)
                return Unauthorized("Invalid username or password.");

            var token = _jwtService.GenerateToken(dbUser.Username, dbUser.RoleId.ToString());
            return Ok(new { Token = token, Username = dbUser.Username });
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordViewModel request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

            if (user == null)
                return NotFound("Username not found.");

            return Ok("Username verified. Proceed to reset your password.");
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordViewModel request)
        {
            if (request.NewPassword != request.ConfirmPassword)
                return BadRequest("Passwords do not match.");

            var user = _context.Users.FirstOrDefault(u => u.Username == request.UserName);

            if (user == null)
                return NotFound("User not found.");

            user.Password = PasswordHasher.HashPassword(request.NewPassword);
            user.UpdatedDT = DateTime.UtcNow;

            _context.SaveChanges();

            return Ok("Password has been successfully reset.");
        }
    }
}
