using System;
using System.Collections.Generic;
using IJAW_Developpy.Connector;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace IJAW_Developpy.Pages_Forum
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpThread : PopupPage
    {

        FirebaseConnector firebaseConnector = new FirebaseConnector();
        string _forumId = "";
        public PopUpThread(string forumId)
        {
            _forumId = forumId;
            InitializeComponent();
        }

        private async void Button_Close(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }

        private async void Button_NewThread(object sender, EventArgs e)
        {
            var newPost = (await firebaseConnector.AddThread(1234, _forumId, ThreadHeader.Text, ThreadBody.Text));

            await Navigation.PushAsync(new ForumThread(newPost.ForumId.ToString(), newPost.ThreadId));

            await PopupNavigation.PopAsync(true);


        }
    }
}