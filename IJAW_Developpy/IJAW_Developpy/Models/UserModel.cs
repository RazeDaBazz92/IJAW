using GalaSoft.MvvmLight;

namespace IJAW_Developpy.Models
{
    public class UserModel : ObservableObject
    {
        public UserModel()
        {

        }

        public string Username { get; set; }
        public string Password { get; set; }

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string DateCreated { get; set; }
        public int PostCount { get; set; }
        public int LikeCount { get; set; }
    }
}
