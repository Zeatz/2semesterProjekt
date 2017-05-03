using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanatic_GUI.ViewModel;

namespace Fanatic_GUI.Handler
{
    public class AdminHandler
    {
        public AdminViewModel AdminViewModel { get; }

        public AdminHandler(AdminViewModel adminViewModel)
        {
            AdminViewModel = adminViewModel;
        }

        public void LogOut()
        {
            if (AdminViewModel.DataSingleton.CurrentUser == null) return;
            AdminViewModel.DataSingleton.CurrentUser = null;
            AdminViewModel.NavigationHandler.ToFrontPage();
        }
    }
}
