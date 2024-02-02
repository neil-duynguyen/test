using ManagementNote.Model;
using ManagementNote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ManagementNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static string? UserName;
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_userRepository.GetAll());
            }
            catch
            {
                return NotFound("Does not have any user");
            }
        }

        [HttpPost]
        public IActionResult Create(CreateUser user)
        {
            var _user = _userRepository.GetByUserName(user);
            if (_user == null)
            {
                return Ok(_userRepository.Create(user));
            }
            else
                return Content("This user has been initialized. Initialization failed.");
        }

        [HttpPut]
        public IActionResult UpdateAvata(string url) { 
            var avata = _userRepository.UpdateAvata(url);
            if(avata == true)
                return Ok(avata);
            return BadRequest();
        }

    }
}