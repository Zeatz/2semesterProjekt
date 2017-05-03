using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Eventmaker.Common;
using FanaticAdminApp.Handler;
using FanaticAdminApp.Interfaces;

namespace FanaticAdminApp.ViewModel
{
    public class AppViewModel
    {
        public DataSingleton DataSingleton { get; }
        public INavigationService NavigationHandler { get; }
        public ServiceHandler ServiceHandler { get; }
        public DateTimeOffset DateTimeOffsetFrom { get; set; }
        public bool Kortspil { get; set; }
        public bool Braetspil { get; set; }
        public bool PenAndPaper { get; set; }
        public bool FigurSpil { get; set; }
        public bool Events { get; set; }
        public bool LiveRollespil { get; set; }
        public DateTimeOffset DateTimeOffsetTo { get; set; }
        public ICommand MakeGraphCommand { get; set; }

        public AppViewModel()
        {
            DataSingleton = DataSingleton.GetInstance;
            ServiceHandler = new ServiceHandler(this);
            NavigationHandler = new NavigationHandler();
            MakeGraphCommand = new RelayCommand(ServiceHandler.GraphSort);
            DateTimeOffsetFrom = new DateTimeOffset(DateTime.Now.Date);
            DateTimeOffsetTo = new DateTimeOffset(DateTime.Now.Date);
        }
    }
}
