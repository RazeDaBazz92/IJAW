using IJAW_Developpy.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IJAW_Developpy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            InitializeComponent();

            BindingContext = new AuthPageViewModel();

        }
    }
}
