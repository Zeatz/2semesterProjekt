using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FanaticAdminApp.Model;
using FanaticAdminApp.Persistency;
using FanaticAdminApp.View;
using FanaticAdminApp.ViewModel;

namespace FanaticAdminApp.Handler
{
    public class ServiceHandler
    {
        public AppViewModel AppViewModel { get; }
        public string ServerUri { get; } = "http://fanaticprojekt.azurewebsites.net/";
        public string StatsApi { get; } = "api/StatsJoinedIrs";
        public ServiceHandler(AppViewModel appViewModel)
        {
            AppViewModel = appViewModel;
            GraphInit();
        }

        public async Task GraphInit()
        {
            try
            {
                AppViewModel.DataSingleton.Statistics = await SmartClient.Get<StatsJoinedIr>(ServerUri, StatsApi);
            }
            catch (Exception e)
            {
                await new MessageDialog(e.Message).ShowAsync();
            }
        }

        public async void GraphSort()
        {
            try
            {
                if (!SmartClient.CheckForInternetConnection())
                    throw new EndOfStreamException("Check om der evt er en internet forbindelse");
                
                if (AppViewModel.DataSingleton.Statistics != null)
                {
                    AppViewModel.DataSingleton.CurrentGraph.Clear();
                    var dateFrom = AppViewModel.DateTimeOffsetFrom.Date;
                    var dateTo = AppViewModel.DateTimeOffsetTo.Date;

                    Dictionary<int, bool> bools = new Dictionary<int, bool>()
                    {
                        {1, AppViewModel.Braetspil},
                        {3, AppViewModel.PenAndPaper},
                        {2, AppViewModel.FigurSpil},
                        {4, AppViewModel.Kortspil},
                        {5,  AppViewModel.LiveRollespil},
                        {6, AppViewModel.Events},
                    };
                    if (bools.Count(x => !x.Value) == bools.Count)
                        throw new ArgumentException("Vælg venligst en eller flere kategorier!");

                    var valgteInteresser = bools.Where(x => x.Value).Select(x => x.Key).ToList();
                    List<List<StatsJoinedIr>> mergedList = new List<List<StatsJoinedIr>>();
                    foreach (var interesse in valgteInteresser)
                    {
                        var gp = AppViewModel.DataSingleton.Statistics.Where(x => x.IR_TYPE == interesse && x.DATE.Date <= dateTo && x.DATE.Date >= dateFrom).ToList();
                        mergedList.Add(gp);
                    }
                    foreach (var liste in mergedList)
                    {
                        AppViewModel.DataSingleton.CurrentGraph.Add(new GraphData()
                        { Count = liste.Count, Name = liste.FirstOrDefault()?.TYPE });
                    }
                }
                else
                {
                    await GraphInit();
                    GraphSort();
                }
            }
            catch (Exception e)
            {
                await new MessageDialog(e.Message).ShowAsync();
            }
        }
    }
}
