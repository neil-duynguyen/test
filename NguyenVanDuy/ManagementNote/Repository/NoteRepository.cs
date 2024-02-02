using ManagementNote.Model;
using Microsoft.AspNetCore.Mvc;

namespace ManagementNote.Services
{
    public class NoteRepository : INoteRepository
    {
        private readonly MyDbContext _context;

        public NoteRepository(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ViewNote Create(CreateNote createNote)
        {
            var UserN = ManagementNote.Services.AccountRepository.UserLogin.UserName;
            var user = _context.userDb.SingleOrDefault(x => x.UserName.Equals(UserN));
            if (user.Status == true)
            {
                try
                {
                    var note = new Note
                    {
                        UserName = UserN,
                        Title = createNote.Title,
                        Content = createNote.Content,
                        DateCreate = DateTime.Now,
                        DateUpdate = DateTime.Now,
                    };
                    _context.Add(note);
                    _context.SaveChanges();
                    return new ViewNote
                    {
                        UserName = UserN,
                        Title = note.Title,
                        Content = note.Content,
                        Date = note.DateCreate
                    };
                }

                catch
                {
                    return null;
                }
            }
            return null;
        }

        public bool Delete(int id)
        {
            var _note = _context.noteDb.SingleOrDefault(x => x.Id == id);
            if (_note != null) { 
                _context.noteDb.Remove(_note);
                _context.SaveChanges();
                return true;
            }
            return false;

        }

        [HttpGet]
        public List<Note> GetAll()
        {
            var listNot = _context.noteDb.ToList<Note>();
            return listNot;
        }

        [HttpPut]
        public Boolean Update(int id, CreateNote createNote)
        {
            var _note = _context.noteDb.SingleOrDefault(x => x.Id == id);
            if (_note != null)
            {
                _note.Title = createNote.Title;
                _note.Content = createNote.Content;
                _note.DateUpdate =  DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
