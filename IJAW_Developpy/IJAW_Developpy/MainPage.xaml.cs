using IJAW_Developpy.ViewModel;
using IJAW_Developpy.Views;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace IJAW_Developpy
{

    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() == typeof(AuthPageViewModel))
                {
                    Navigation.RemovePage(page);
                    break;
                }
            }
        }
        private async void BtnForum_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForumOverview());
        }
    }
}
