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
using Microsoft.EntityFrameworkCore;
using R.BAL.Services.Interface;

namespace NTireRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly RestaurantDbContext _context;
        private readonly IConfiguration _config;
        private readonly JWTService _jwtService;
        private readonly IUserService _userService;

        public AccountController(RestaurantDbContext context, IConfiguration config, JWTService jwtService,IUserService userService)
        {
            _context = context;
            _config = config;
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTOs userViewModel)
        {

            try
            {
                var usernameExists = await _userService.IsUsernameExists(userViewModel.Username);
                if (usernameExists)
                {
                    return BadRequest("Username already exists. Please choose a different username.");
                }

                var response = await _userService.RegisterUser(userViewModel);

                if (!response)
                {
                    return StatusCode(500, "An error occurred while registering the user. Please try again later.");
                }

                return Ok("Registered successful.");
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Error: {dbEx.InnerException?.Message}");

                return StatusCode(500, "Database update failed. Please try again later.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An internal error occurred. Please try again later.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserDTOs userViewModel)
        {
            try
            {
                var dbUser = await _userService.GetUserByUsername(userViewModel.Username);

                if (dbUser == null || dbUser.Password != userViewModel.Password) 
                {
                    return Unauthorized("Invalid username or password.");
                }

                // Generate token
                var token = _jwtService.GenerateToken(dbUser.Username, dbUser.RoleId.ToString());
                return Ok(new { Token = token, Username = dbUser.Username });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An internal error occurred. Please try again later.");
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordDTOs request)
        {
            try
            {
                var user = await _userService.GetUserByUsername(request.Username);

                if (user == null)
                    return NotFound("Username not found.");

                return Ok("Username verified. Proceed to reset your password.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An internal error occurred. Please try again later.");
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDTOs request)
        {
            try
            {
                if (request.NewPassword != request.ConfirmPassword)
                    return BadRequest("Passwords do not match.");

                var user = await _userService.GetUserByUsername(request.UserName);

                if (user == null)
                    return NotFound("User not found.");

                return Ok("Password has been successfully reset.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An internal error occurred. Please try again later.");
            }
        }
    }
}
