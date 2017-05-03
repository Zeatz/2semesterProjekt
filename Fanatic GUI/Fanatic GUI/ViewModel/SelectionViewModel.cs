using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Eventmaker.Common;
using Fanatic_GUI.Annotations;
using Fanatic_GUI.Handler;
using Fanatic_GUI.Interfaces;

namespace Fanatic_GUI.ViewModel
{
    public class SelectionViewModel : INotifyPropertyChanged
    {
        private Visibility _isButtonVisible;
        public bool Figurspilvalgt { get; set; }
        public bool PenAndPaperValgt { get; set; }
        public bool EventsValgt { get; set; }
        public bool KortspilValgt { get; set; }
        public bool LiveRollespilValgt { get; set; }
        public bool BraetspilValgt { get; set; }
        public DataSingleton DataSingleton { get; }
        public INavigationHandler NavigationHandler { get; }
        public ServiceHandler ServiceHandler { get; }
        public ICommand SaveCommand { get; }
        public ICommand ToFrontPageCommand { get; }
        public Visibility IsButtonVisible
        {
            get { return _isButtonVisible; }
            set { _isButtonVisible = value; OnPropertyChanged(); }
        }
        public SelectionViewModel()
        {
            IsButtonVisible = Visibility.Visible;
            DataSingleton = DataSingleton.GetInstance;
            NavigationHandler = new NavigationHandler();
            ServiceHandler = new ServiceHandler(this);
            SaveCommand = new RelayCommand(ServiceHandler.Save);
            ToFrontPageCommand = new RelayCommand(NavigationHandler.ToFrontPage);
        }






        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
