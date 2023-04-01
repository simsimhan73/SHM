using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHM.Interfaces
{
    internal interface INavigationAware
    {
        void OnNavigating(object sender, object navigationEventArgs);
        void OnNavigated(object sender, object navigatedEventArgs);
    }
}
