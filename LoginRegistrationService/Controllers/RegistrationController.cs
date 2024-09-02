using LoginRegistrationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Net;
using System.Runtime.Intrinsics.X86;

namespace LoginRegistrationService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private readonly RegistrationRepository _userRepository;

        public RegistrationController(RegistrationRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("Signup")]
        public IActionResult Registration([FromForm]string email, [FromForm] string password)
        {
            var user = _userRepository.Users.FirstOrDefault(u => u.email == email && u.password == password);
            if (user == null)
            {
                var user1 = new Registration { email = email, password = password };
                _userRepository.Users.Add(user1);
                _userRepository.SaveChanges();
                return Ok(user1.id_user);
            }
            else
            {
                return NotFound("This user already exists. Login please!");
            }
        }

        [HttpPost("Login")]
        public IActionResult LoginAsync([FromForm] string email, [FromForm] string password)
        {
            var userExists = _userRepository.Users.Any(u => u.email == email && u.password == password);
            if (userExists)
            {
                var user = _userRepository.Users.FirstOrDefault(u => u.email == email && u.password == password);
                return Ok(user.id_user);
            }
            else
            {
                return NotFound("User not found. Sign up please!");
            }
        }
    }
}