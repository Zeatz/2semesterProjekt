using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FanaticAdminApp.Model;

namespace FanaticAdminApp.Handler
{
    public class DataSingleton
    {
        private object locker = new object();
        private List<StatsJoinedIr> _statistics;
        public static DataSingleton GetInstance { get; } = new DataSingleton();

        public List<StatsJoinedIr> Statistics
        {
            get { return _statistics; }
            set
            {
                lock (locker)
                {
                    _statistics = value;
                }
            }
        }

        public ObservableCollection<GraphData> CurrentGraph { get; set; }
        private DataSingleton()
        {
            Statistics = new List<StatsJoinedIr>();
            CurrentGraph = new ObservableCollection<GraphData>();
        }



    }
}
