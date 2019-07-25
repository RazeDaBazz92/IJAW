
namespace IJAW_Developpy.Models
{
    public class Thread
    {
        public string CategoryName { get; set; }
        public string ThreadId { get; set; }
        public int ForumId { get; set; }
        public string Header { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public int AuthorId { get; set; }
        public int UpdatedByAuthorId { get; set; }
        public bool Sticky { get; set; }
        public int PostCount { get; set; }
        public int ViewCount { get; set; }
    }
}
