using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Eventmaker.Common;
using Fanatic_GUI.Handler;
using Fanatic_GUI.Interfaces;

namespace Fanatic_GUI.ViewModel
{
    public class GraphViewModel
    {
        public bool Braetspil { get; set; }
        public bool PenAndPaper { get; set; }
        public bool FigurSpil { get; set; }
        public bool Kortspil { get; set; }
        public bool LiveRollespil { get; set; }
        public bool Events { get; set; }
        public DateTimeOffset DateTimeOffsetFrom { get; set; }
        public DateTimeOffset DateTimeOffsetTo { get; set; }
        public ICommand MakeGraphCommand { get; }
        public INavigationHandler NavigationHandler { get; }
        public GraphHandler GHandler { get; }
        public DataSingleton DataSingleton { get; }
        public ICommand ToAdminCommand { get; }

        public GraphViewModel()
        {
            NavigationHandler = new NavigationHandler();
            GHandler = new GraphHandler(this);
            DataSingleton = DataSingleton.GetInstance;
            DateTimeOffsetFrom = new DateTimeOffset(DateTime.Now.Date);
            DateTimeOffsetTo = new DateTimeOffset(DateTime.Now.Date);
            MakeGraphCommand = new RelayCommand(GHandler.GraphSort);
            ToAdminCommand = new RelayCommand(NavigationHandler.ToAdminView);
        }
    }
}
