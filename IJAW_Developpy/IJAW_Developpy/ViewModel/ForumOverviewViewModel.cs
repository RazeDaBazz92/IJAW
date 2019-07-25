using IJAW_Developpy.Connector;
using IJAW_Developpy.Models;
using IJAW_Developpy.Pages_Forum;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IJAW_Developpy.ViewModel
{
    public class ForumOverviewViewModel : INotifyPropertyChanged
    {
        FirebaseConnector firebaseConnector = new FirebaseConnector();
        public event PropertyChangedEventHandler PropertyChanged;
        private Command _forumCommand;
        private List<Category> _listOfCategories;
        private bool _isBusy;

        public ForumOverviewViewModel()
        {
            ForumCommand = new Command(GoToForumCategory, param => true);
        }

        public async Task<List<Category>> Load()
        {
            IsBusy = true;
            var allCategories = await firebaseConnector.GetAllCategories();
            if (allCategories.Any())
            {
                IsBusy = false;
                return allCategories;
            }
            else
            {
                IsBusy = false;
                return null;
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public Command ForumCommand
        {
            get
            {
                return _forumCommand;
            }
            set
            {
                _forumCommand = value;
            }
        }

        public async void GoToForumCategory(object obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ForumCategory(obj.ToString()));
        }

        public List<Category> ListOfCategories
        {
            get
            {
                return _listOfCategories;
            }
            set
            {
                _listOfCategories = value;
                OnPropertyChanged();
            }
        }


        public void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
