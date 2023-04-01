using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHM.ViewModel
{
    internal class StartUpViewModel : ViewModelBase
    {
        public static int Count { get; set; }

        public StartUpViewModel()
        {
            Title = "Home";
        }

        public override void OnNavigated(object sender, object navigatedEventArgs)
        {
            Count++;
            Message = $"{Count} Navigated";
        }
    }
}
