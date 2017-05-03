using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Eventmaker.Common;
using Fanatic_GUI.Annotations;
using Fanatic_GUI.Handler;
using Fanatic_GUI.Interfaces;
using Fanatic_GUI.Model;
using Fanatic_GUI.View;

namespace Fanatic_GUI.ViewModel
{
    public class FrontViewModel
    {
        public ICommand ToSelectionPageCommand { get; }
        public ICommand ToLoginPageCommand { get; }
        public INavigationHandler NavigationHandler { get; }
        public FrontViewModel()
        {
            NavigationHandler = new NavigationHandler();
            ToSelectionPageCommand = new RelayCommand(NavigationHandler.ToSelectionPage);
            ToLoginPageCommand = new RelayCommand(NavigationHandler.ToLoginPage);
        }
    }
}