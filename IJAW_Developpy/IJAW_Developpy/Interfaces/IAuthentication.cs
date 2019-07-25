using System;

namespace IJAW_Developpy.Interfaces
{
    public interface IAuthentication
    {
        void SignUp(string email, string password);
        void Login(string email, string password);
        void SignOut();
        bool IsBusy();
        event EventHandler<FirebaseAuthEventData> CustomAuthStateChanged;
    }

}
