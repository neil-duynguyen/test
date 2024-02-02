using ManagementNote.Model;

namespace ManagementNote.Services
{
    public interface INoteRepository
    {
        List<Note> GetAll();
        ViewNote Create(CreateNote createNote);
        Boolean Update(int id, CreateNote createNote);
        Boolean Delete(int id);
    }
}
