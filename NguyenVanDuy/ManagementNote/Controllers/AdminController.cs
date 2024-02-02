using ManagementNote.Model;
using ManagementNote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementNote.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminRepository _adminRepository;
        private readonly MyDbContext _context;

        public AdminController(IAdminRepository adminRepository, MyDbContext context)
        {
            _adminRepository = adminRepository;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            var user = _adminRepository.GetAllUser();
            return Ok(user);
        }

        [HttpPut]
        public IActionResult UpdateRole(string userN, string role) {
            var user = _adminRepository.Update(userN, role);
            return Ok(user);
        }

        [HttpGet("/GetAllNote")]
        public IActionResult GetAllNote() { 
            var notes =_context.noteDb.ToList().Select(c => new { c.UserName, c.Title, c.Content, c.DateCreate}).GroupBy(v => v.UserName);
            return Ok(notes);
        }

        [HttpPut("/Lock")]
        public IActionResult Lock(string userName, Boolean b) { 
            
            var tmp = _adminRepository.Lock(userName, b);
            return Ok(new { User = userName, Status = b});
        }

    }
}
