using CommunityToolkit.Mvvm.ComponentModel;
using SHM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHM.ViewModel
{
    internal class ViewModelBase : ObservableObject, INavigationAware
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _message;
        /// <summary>
        /// 메시지
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public virtual void OnNavigated(object sender, object navigatedEventArgs)
        {
        }

        public virtual void OnNavigating(object sender, object navigationEventArgs)
        {
        }
    }
}
