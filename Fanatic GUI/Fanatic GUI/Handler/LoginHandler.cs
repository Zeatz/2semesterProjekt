using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Fanatic_GUI.Exceptions;
using Fanatic_GUI.Persistency;
using Fanatic_GUI.ViewModel;

namespace Fanatic_GUI.Handler
{
    public class LoginHandler
    {
        public LoginViewModel LoginViewModel { get; }

        public LoginHandler(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }

        public async void Login()
        {
            try
            {
                // tjekker om der ikke er internet, mangler der forbindelse så kastes en fejl
                if (!SmartClient.CheckForInternetConnection())
                {
                    throw new NoInternetException("Kontakt medarbejder, intet internet!");
                }

                // prøver at hente en user, udfra det givne username og password
                var user = await PersistencyClass.AuthUser(LoginViewModel.UserName, LoginViewModel.Password);
                // hvis den finder en bruger kan vi logge den ind, ellers eksistere brugeren ikke
                if (user != null)
                {
                    // tjekker om brugeren har administrator rettigheder eller over bruger niveau
                    if (user.user_status > 9)
                    {
                        LoginViewModel.DataSingleton.CurrentUser = user;
                        LoginViewModel.Password = null;
                        LoginViewModel.UserName = null;
                        LoginViewModel.NavigationHandler.ToAdminView();
                        return;
                    }
                    // normalt login og navigering til Selektionssiden
                    LoginViewModel.DataSingleton.CurrentUser = user;
                    LoginViewModel.Password = null;
                    LoginViewModel.UserName = null;
                    LoginViewModel.NavigationHandler.ToSelectionPage();
                    // plus nogle points til deres kundeklub ting
                }
                else
                {
                    throw new LoginException("Username eller password matcher ingen eksisterende brugere.");
                }
            }
            catch (Exception e)
            {
                await new MessageDialog(e.Message).ShowAsync();
            }
        }

    }
}
