using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Fanatic_GUI.Interfaces;
using Fanatic_GUI.View;
using Fanatic_GUI.ViewModel;

namespace Fanatic_GUI.Handler
{
    /// <summary>
    /// en class der håndtere navigation i appen, både med hardcoded metoder og uden.
    /// </summary>
    public class NavigationHandler : INavigationHandler
    {
        public void ToSelectionPage()
        {
            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(View.formaalSelection));
        }
        public void ToAdminView()
        {
            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(View.AdminView));
        }
        public void ToFrontPage()
        {
            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(View.HomePage));
        }

        public void ToLoginPage()
        {
            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(View.LoginPage));
        }

        public void ToGraphPage()
        {
            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof (View.GraphPanel));
        }
        public void GoBack()
        {
            var frame = Window.Current.Content as Frame;
            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        public void GoForward()
        {
            var frame = Window.Current.Content as Frame;
            if (frame.CanGoForward)
            {
                frame.GoForward();
            }
        }

        public void NavigateTo(Type sourceType)
        {
            var frame = Window.Current.Content as Frame;
            try
            {
                frame.Navigate(sourceType);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Cannot navigate the that page.");
            }
        }
    }
}
