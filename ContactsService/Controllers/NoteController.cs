using NotesService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace NotesService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NotesRepository _noteRepository;

        public NoteController(NotesRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
        [HttpPost("SaveUser")]
        public IActionResult SaveUser([FromBody] int user)
        {
            var notes = _noteRepository.Notes.Where(c => c.id_user == user).ToList();

            return RedirectToAction("getAllNotes", new { id_user = user });
        }

        [HttpGet("Note")]
        public IActionResult getAllNotes([FromQuery] int id_user)
        {
            try
            {
                var notes = _noteRepository.Notes.Where(c => c.id_user == id_user).ToList();
                if (notes.Count == 0)
                {
                    return Ok(id_user);
                }
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("Note/id")]
        public IActionResult getNote(int id)
        {
            try
            {
                var note = _noteRepository.Notes.Find(id);
                if (note == null)
                {
                    return NotFound("Note not found");
                }
                return Ok(note);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Note")]
        public IActionResult AddNote(Note request)
        {
            try
            {
                _noteRepository.Add(request);
                _noteRepository.SaveChanges();
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Note")]
        public IActionResult UpdateNote(Note request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Invalid data");
                }
                var note = _noteRepository.Notes.Find(request.note_id);
                if (note == null)
                {
                    return NotFound($"{request.note_id} is not found");
                }
                note.note_name = request.note_name;
                note.description = request.description;
                note.note_create = request.note_create;
                _noteRepository.SaveChanges();
                return Ok(note);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Note")]
        public IActionResult DeleteNote(int id)
        {
            try
            {
                var note = _noteRepository.Notes.Find(id);
                if (note == null)
                {
                    return NotFound($"{id} is not found");
                }
                _noteRepository.Remove(note);
                _noteRepository.SaveChanges();
                return Ok(note);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
