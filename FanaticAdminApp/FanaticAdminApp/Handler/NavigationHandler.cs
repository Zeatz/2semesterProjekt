using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FanaticAdminApp.Interfaces;

namespace FanaticAdminApp.Handler
{
    public class NavigationHandler : INavigationService
    {
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
