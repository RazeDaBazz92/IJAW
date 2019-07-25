using System;
using System.Linq;
using IJAW_Developpy.Connector;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace IJAW_Developpy.Pages_Forum
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpPost : PopupPage
    {
        FirebaseConnector firebaseConnector = new FirebaseConnector();
        string _forumId = "";
        string _threadName = "";
        string _threadId = "";
        public PopUpPost(string forumId, string threadId, string threadName)
        {
            _forumId = forumId;
            _threadId = threadId;
            _threadName = threadName;
            InitializeComponent();
        }

        private async void Button_Close(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }

        private async void Button_NewPost(object sender, EventArgs e)
        {
            await firebaseConnector.AddPost(1234, _forumId, _threadId, _threadName, PostBody.Text);
            await Navigation.PushAsync(new ForumThread(_forumId, _threadId));

            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() == typeof(ForumThread))
                {
                    Navigation.RemovePage(page);
                    break;
                }
            }

            await PopupNavigation.PopAsync(true);
        }
    }
}