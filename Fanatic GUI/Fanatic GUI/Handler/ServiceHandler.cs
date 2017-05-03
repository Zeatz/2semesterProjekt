using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Fanatic_GUI.Exceptions;
using Fanatic_GUI.Model;
using Fanatic_GUI.Persistency;
using Fanatic_GUI.ViewModel;


namespace Fanatic_GUI.Handler
{
    public class ServiceHandler
    {
        // gæste ID fra databasen
        private int _guestID = 3;
        public SelectionViewModel SelecViewModel { get; }
   
        public ServiceHandler(SelectionViewModel selecViewModel)
        {
            SelecViewModel = selecViewModel;
        }

        public async void Save()
        {
            try
            {
                // gemmer knappen, så vi kan undgå dobbelt klik
                SelecViewModel.IsButtonVisible = Visibility.Collapsed;
                // tjekker om der ikke er internet, mangler der forbindelse så kastes en fejl
                if (!SmartClient.CheckForInternetConnection())
                {
                    throw new NoInternetException("Kontakt medarbejder, intet internet!");
                }
                // finder ud af om der er en der er logget ind, ellers sætter den user til _guestID
                var user = SelecViewModel.DataSingleton.CurrentUser?.ID ?? _guestID;

                // opretter en dictionary med det valgte bools
                // istedet for en int bool kunne vi have brugt enums af interesser istedet for int
                // dog har vi ikke haft meget erfaring med enums
                Dictionary<int, bool> bools = new Dictionary<int, bool>()
                {
                    {1, SelecViewModel.BraetspilValgt},
                    {3, SelecViewModel.PenAndPaperValgt},
                    {2, SelecViewModel.Figurspilvalgt},
                    {4, SelecViewModel.KortspilValgt},
                    {5, SelecViewModel.LiveRollespilValgt},
                    {6, SelecViewModel.EventsValgt},
                };
                // opretter et DTO med dictionary'en og id'et
                StatisticsDTO dto = new StatisticsDTO() {BooleanDic = bools, ID = user};
                // sender dto'et i en post request til webserveren
                var httpResponseMessage = await PersistencyClass.PostUserReviews(dto);
                // hvis statuscode er notfound så fejlmeddelelse, evt webserver fejl
                if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NoInternetException("Noget gik galt.\nKontakt medarbejder");
                }
                // hvis httpResponseMessage returnere succescode
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // opretter et stackpanel og tilføjer et image fra assests
                    var panel = new StackPanel();
                    panel.Children.Add(new Image()
                    {
                        Visibility = Visibility.Visible,
                        Source = new BitmapImage(new Uri("ms-appx:///Assets/orken.png", UriKind.RelativeOrAbsolute)),
                        Stretch = Stretch.Fill,
                        MaxHeight = 400,
                    });
                    // opretter en contentdialog, som har panel som content
                    var cd = new ContentDialog
                    {
                        MaxHeight = 450,
                        MaxWidth = 400,
                        IsPrimaryButtonEnabled = true,
                        PrimaryButtonText = "OK",
                        Content = panel,
                    };
                    // logger bruger eller gæst ud
                    SelecViewModel.DataSingleton.CurrentUser = null;
                    // tilbage til frontpage
                    SelecViewModel.NavigationHandler.ToFrontPage();
                    // vis contentdialog
                    await cd.ShowAsync();
                    SelecViewModel.IsButtonVisible = Visibility.Visible;
                }
            }
            catch (Exception e)
            {
                SelecViewModel.IsButtonVisible = Visibility.Visible;
                await new MessageDialog(e.Message).ShowAsync();
            }
        }
    }
}