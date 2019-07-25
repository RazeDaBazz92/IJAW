using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IJAW_Developpy.ViewModel;
using IJAW_Developpy.Connector;
using System.Linq;

namespace IJAW_Developpy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumOverview : ContentPage
    {
        FirebaseConnector firebaseConnector = new FirebaseConnector();
        public ForumOverview()
        {
            InitializeComponent();

            BindingContext = new ForumOverviewViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Load();

        }

        public async void Load()
        {
            IsBusy = true;
            var allCategories = await firebaseConnector.GetAllCategories();
            if (allCategories.Any())
            {
                IsBusy = false;
                CategoryList.ItemsSource = allCategories;
            }
            else
            {
                IsBusy = false;
            }
        }
    }
}