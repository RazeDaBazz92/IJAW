using System.Collections.Generic;

namespace IJAW_Developpy.Models
{
    public class Post
    {
        public string PostId { get; set; }
        public string ThreadId { get; set; }
        public int ForumId { get; set; }
        public string ThreadName { get; set; }
        public string Body { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public int AuthorId { get; set; }
        public List<int> LikeList { get; set; }
    }
}
