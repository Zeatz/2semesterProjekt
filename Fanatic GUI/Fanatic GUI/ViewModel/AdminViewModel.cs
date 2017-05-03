using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Eventmaker.Common;
using Fanatic_GUI.Handler;
using Fanatic_GUI.Interfaces;

namespace Fanatic_GUI.ViewModel
{
    public class AdminViewModel
    {
        public DataSingleton DataSingleton { get; }
        public INavigationHandler NavigationHandler { get; }
        public AdminHandler AdminHandler { get; }
        public ICommand LogOutCommand { get; }
        public ICommand ToGraphPanelCommand { get; }

        public AdminViewModel()
        {
            DataSingleton = DataSingleton.GetInstance;
            AdminHandler = new AdminHandler(this);
            NavigationHandler = new NavigationHandler();
            ToGraphPanelCommand = new RelayCommand(NavigationHandler.ToGraphPage);
            LogOutCommand = new RelayCommand(AdminHandler.LogOut);
        }
    }
}
