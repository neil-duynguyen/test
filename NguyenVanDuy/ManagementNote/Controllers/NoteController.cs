using ManagementNote.Model;
using ManagementNote.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;
        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet]
        //[Authorize(Roles ="user")]
        public IActionResult GetAll()
        {
            var listNote = _noteRepository.GetAll();
            return Ok(listNote);
        }

        [HttpPost]
        
        public IActionResult Create(CreateNote createNote)
        {
            var note = _noteRepository.Create(createNote);

            if (note == null) {
                return Content("Create note failed.");
            }
            return Ok(note);
        }

        [HttpPut("{ID}")]
        public IActionResult Update(int ID, CreateNote createNote) {
            if (_noteRepository.Update(ID, createNote)) {
                return Ok("Update note successfull.");
            }
            return Content("Update note failed.");
        }

        [HttpDelete("{ID}")]
        public IActionResult Delete(int ID)
        {
            if(_noteRepository.Delete(ID))
                return Ok("Delete  note successfull.");
            return NotFound("Not found. Delete note failed.");

        }
    }
}
