using System;
using System.Linq;
using Rg.Plugins.Popup.Services;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IJAW_Developpy.Connector;
using IJAW_Developpy.Models;
using IJAW_Developpy.Interfaces;

namespace IJAW_Developpy.Pages_Forum
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumThread : ContentPage, INotifyPropertyChanged
    {
        FirebaseConnector firebaseConnector = new FirebaseConnector();
        public Post _currentThread;
        public string _threadId = "";
        public string _forumId = "";
        public event PropertyChangedEventHandler PropertyChanged;
        public ForumThread(string forumid, string threadId)
        {
            _threadId = threadId;
            _forumId = forumid;
            _currentThread = new Post();
            InitializeComponent();

            ListAllPosts.RefreshCommand = new Command(() => {
                //Do your stuff.    
                Load();
                ListAllPosts.IsRefreshing = false;
            });
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Load();

        }

        private async void Load()
        {
            var allPosts = await firebaseConnector.GetAllPosts(_forumId, _threadId);
            if (allPosts.Any())
            {
                ListAllPosts.ItemsSource = allPosts;
                Title = allPosts.FirstOrDefault().ThreadName;
                _currentThread = allPosts[0];
            }
        }

        private void BtnNewPost_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopUpPost(_forumId, _threadId, _currentThread.ThreadName));
            //  Load();
            // OnPropertyChanged("Body");
        }
    }
}