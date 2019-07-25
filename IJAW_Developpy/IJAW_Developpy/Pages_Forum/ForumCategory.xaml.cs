using System;
using System.Linq;
using IJAW_Developpy.Connector;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IJAW_Developpy.Pages_Forum
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumCategory : ContentPage
    {
        FirebaseConnector firebaseConnector = new FirebaseConnector();
        public string _forumId = "";
        public ForumCategory(string forumId)
        {
            _forumId = forumId;
            InitializeComponent();

            ListAllThreads.RefreshCommand = new Command(() => {
                Load();
                ListAllThreads.IsRefreshing = false;
            });
        }

        private void BtnForum_Clicked(object sender, EventArgs e)
        {
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() != typeof(MainPage))
                    Navigation.RemovePage(page);
            }
           // Navigation.PushAsync(new ForumOverview());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Load();

        }

        private async void Load()
        {
            var allThreads = await firebaseConnector.GetAllThreads(_forumId);
            if (allThreads.Any())
            {
                ListAllThreads.ItemsSource = allThreads;
                Title = allThreads.FirstOrDefault().CategoryName;
            }
        }

        private async void BtnThread_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;

            await Navigation.PushAsync(new ForumThread(_forumId, button.AutomationId));
        }

        private async void BtnNewThread_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopUpThread(_forumId));
        }
    }
}