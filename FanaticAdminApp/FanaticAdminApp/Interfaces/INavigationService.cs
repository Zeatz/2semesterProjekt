using System;

namespace FanaticAdminApp.Interfaces
{
    public interface INavigationService
    {
        void GoBack();
        void GoForward();
        void NavigateTo(Type sourceType);
    }
}