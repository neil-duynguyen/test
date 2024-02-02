namespace ManagementNote.Model
{
    public class ViewUser
    {
        public ViewUser(string userName, int NumNote, DateTime lastlogin, DateTime dateCreated, string? avata)
        {
            UserName = userName;
            this.NumNote = NumNote;
            Lastlogin = lastlogin;
            DateCreated = dateCreated;
            Avata = avata;
        }

        public string UserName { get; set; }
        public int NumNote { get; set; }
        public DateTime Lastlogin { get; set; }
        public DateTime DateCreated { get; set; }
        public string? Avata { get; set; } = null;

    }
}
