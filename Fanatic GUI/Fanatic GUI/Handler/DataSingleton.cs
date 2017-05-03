using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fanatic_GUI.Model;

namespace Fanatic_GUI.Handler
{
    public class DataSingleton
    {
        public static DataSingleton GetInstance { get; } = new DataSingleton();

        // Statistics skal ikke initialiseres, da vi henter en liste af data, den tildeles
        // dvs at den kun eksistere hvis den skal bruges.
        public List<StatsJoinedIr> Statistics { get; set; }

        public ObservableCollection<GraphData> CurrentGraph { get; }
        public UserTable CurrentUser { get; set; }
        private DataSingleton()
        {
            CurrentGraph = new ObservableCollection<GraphData>();
        }



    }
}
