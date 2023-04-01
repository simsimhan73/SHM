using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using SHM.Model;

namespace SHM.Core
{
    internal class SeatCore
    {
        public static void Suffle<T>(ref ObservableCollection<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            while(n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
