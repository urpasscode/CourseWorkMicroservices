using AccountService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserRepository _user;

        public AccountController(UserRepository userRepository)
        {
            _user = userRepository;
        }

        [HttpPost("SaveUser")]
        public IActionResult SaveUser([FromBody] int user)
        {
            var one_user = _user.Users.Where(c => c.id_user == user).ToList();

            //return Ok(contacts);
            return RedirectToAction("getUser", new { id_user = user });
        }
        [HttpGet("Account")]
        public IActionResult GetUser(int id_user)
        {
            var user = _user.Users.FirstOrDefault(u => u.id_user == id_user);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Возврат данных пользователя
            return Ok(user);
        }

        [HttpPut("Account")]
        public IActionResult UpdateUser(Account request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Invalid data");
                }
                var user = _user.Users.Find(request.id_user);
                if (user == null)
                {
                    return NotFound($"{request.id_user} is not found");
                }
                user.email = request.email;
                user.password = request.password;
                _user.SaveChanges();
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Account")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _user.Users.Find(id);
                if (user == null)
                {
                    return NotFound($"{id} is not found");
                }
                _user.Remove(user);
                _user.SaveChanges();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}