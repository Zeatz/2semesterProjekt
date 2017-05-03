using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Animation;
using Eventmaker.Common;
using Fanatic_GUI.Annotations;
using Fanatic_GUI.Handler;
using Fanatic_GUI.Interfaces;

namespace Fanatic_GUI.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _password;
        private string _userName;
        public LoginHandler LHandler { get; }
        public DataSingleton DataSingleton { get; }
        public INavigationHandler NavigationHandler { get; }
        public ICommand LoginCommand { get; }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        public ICommand ToFrontPageCommand { get; }
        public LoginViewModel()
        {
            LHandler = new LoginHandler(this);
            NavigationHandler = new NavigationHandler();
            DataSingleton = DataSingleton.GetInstance;
            LoginCommand = new RelayCommand(LHandler.Login);
            ToFrontPageCommand = new RelayCommand(NavigationHandler.ToFrontPage);
        }
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
