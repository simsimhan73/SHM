using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SHM.Model;

namespace SHM.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private string _navigationSource;

        public string NavigationSource
        {
            get { return _navigationSource; }
            set { SetProperty(ref _navigationSource, value); }
        }

        public ICommand NavigateCommand { get; set; }

        public MainWindowViewModel()
        {
            Title = "Main View";
            Init();
        }    

        private void Init()
        {
            NavigationSource = "View/StartUp.xaml";
            NavigateCommand = new RelayCommand<string>(OnNavigate);

            WeakReferenceMessenger.Default.Register<NavigationMessage>(this, OnNavigationMessage);
        }

        private void OnNavigationMessage(object recipient, NavigationMessage message)
        {
            NavigationSource = message.Value;
        }

        private void OnNavigate(string pageUri)
        {
            NavigationSource = pageUri;
        }
    }
}
