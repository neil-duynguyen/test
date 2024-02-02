namespace ManagementNote.Model
{
    public class Note
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        //referen
        public User user { get; set; }
    }
}
