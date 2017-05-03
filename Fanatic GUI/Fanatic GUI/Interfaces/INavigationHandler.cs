using System;
using Windows.System;

namespace Fanatic_GUI.Interfaces
{
    public interface INavigationHandler
    {
        void ToSelectionPage();
        void ToFrontPage();
        void ToAdminView();
        void ToLoginPage();
        void GoBack();
        void ToGraphPage();
        void GoForward();
        void NavigateTo(Type sourceType);
    }
}