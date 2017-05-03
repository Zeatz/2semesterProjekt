using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Fanatic_GUI.Exceptions;
using Fanatic_GUI.Model;
using Fanatic_GUI.Persistency;
using Fanatic_GUI.ViewModel;

namespace Fanatic_GUI.Handler
{
    public class GraphHandler
    {
        public GraphViewModel GraphViewModel { get; }
        public string ServerUri { get; } = "http://fanaticprojekt.azurewebsites.net/";
        public string StatsApi { get; } = "api/StatsJoinedIrs";

        public GraphHandler(GraphViewModel graphViewModel)
        {
            GraphViewModel = graphViewModel;
        }

        public async Task GraphInit()
        {
            try
            {
                // henter data'en og fylder statistics listen op, så den reference peger på en liste
                GraphViewModel.DataSingleton.Statistics = await SmartClient.Get<StatsJoinedIr>(ServerUri, StatsApi);
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
                // tjekker om der ikke er internet, mangler der forbindelse så kastes en fejl
                if (!SmartClient.CheckForInternetConnection())
                    throw new NoInternetException("Check om der evt er en internet forbindelse");

                // hvis statistics listen i datasingleton er null, skal den hente data'en først
                if (GraphViewModel.DataSingleton.Statistics != null)
                {
                    // clear fordi vi ikke vi er interesseret i en ny graf hver gang
                    GraphViewModel.DataSingleton.CurrentGraph.Clear();
                    // tager dato'erne fra viewet og gemmer dem i variabler
                    var dateFrom = GraphViewModel.DateTimeOffsetFrom.Date;
                    var dateTo = GraphViewModel.DateTimeOffsetTo.Date;

                    // henter de valgte kategorier fra viewet
                    Dictionary<int, bool> bools = new Dictionary<int, bool>()
                    {
                        {1, GraphViewModel.Braetspil},
                        {3, GraphViewModel.PenAndPaper},
                        {2, GraphViewModel.FigurSpil},
                        {4, GraphViewModel.Kortspil},
                        {5, GraphViewModel.LiveRollespil},
                        {6, GraphViewModel.Events},
                    };

                    // tjekker om der ikke er valgt nogen
                    if (bools.Count(x => !x.Value) == bools.Count)
                        throw new ArgumentException("Vælg venligst en eller flere kategorier!");

                    // laver en liste af ints, som representere de valgte kategorier
                    var valgteInteresser = bools.Where(x => x.Value).Select(x => x.Key).ToList();
                    List<List<StatsJoinedIr>> mergedList = new List<List<StatsJoinedIr>>();
                    foreach (var interesse in valgteInteresser)
                    {
                        // sortere og lavet en liste af StatsJoinedIr, der opfylder de andre valgte krav
                        // derefter tilføjer den listen til den mergedList
                        var gp =
                            GraphViewModel.DataSingleton.Statistics.Where(
                                x => x.IR_TYPE == interesse && x.DATE.Date <= dateTo && x.DATE.Date >= dateFrom)
                                .ToList();
                        mergedList.Add(gp);
                    }
                    foreach (var liste in mergedList)
                    {
                        // laves GraphData og bliver tilføjet til currentGraph som er en observablecollection
                        // denne collection er bindet til Syncfusion frameworkets Charts control's itemsource
                        // et GraphData objekt er lavet udfra en mængde Count som er den enkle liste's count
                        // og Name er navnet på kategorien som den enkle liste representere
                        GraphViewModel.DataSingleton.CurrentGraph.Add(new GraphData()
                        { Count = liste.Count, Name = liste.FirstOrDefault()?.TYPE });
                    }
                }
                else
                {
                    // Hvis data'en til sorteringen endnu ikke er hentet
                    // så vil dette blive hentet
                    await GraphInit();
                    // derefter et rekursivt kald, så metoden køres igen med samme argumenter
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
