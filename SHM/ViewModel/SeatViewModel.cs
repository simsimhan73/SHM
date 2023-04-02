using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SHM.Model;
using SHM.Core;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace SHM.ViewModel
{
    class SeatViewModel : ViewModelBase
    {
        public ICommand RunCommand { get; set; }
        public ICommand PDFCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand LoadCommand { get; set; }

        private string _inputname;
        private ObservableCollection<ChairModel> _chairs;
        private ClassRoom _room;
        private int _nowcol;
        private int _nowrow;

        public SeatViewModel()
        {
            _inputname = "";
            _chairs = new ObservableCollection<ChairModel>();
            _room = new ClassRoom();

            _nowcol = 0;
            _nowrow = 0;
            
            RunCommand = new RelayCommand(Run);
            PDFCommand = new RelayCommand(PDF);
            SaveCommand = new RelayCommand(Save);
            LoadCommand = new RelayCommand(Load);
        }

        private void Save()
        {
            SeatCore.Save(_chairs, _room.Height, _room.Width);
        }

        private void Load()
        {
            SeatCore.Load(ref _chairs, _room.Height, _room.Width);
        }

        private void PDF()
        {
            SeatCore.CreatePDF(SeatCore.STOL(_chairs, _room.Height, _room.Width), _room.Width, _room.Height);
        }

        private void Run()
        {
            int n = _chairs.Count - 1;
            ObservableCollection<ChairModel> list = new ObservableCollection<ChairModel>(); 
            while(n > 1)
            {
                if (_chairs[n].Checked) list.Add(_chairs[n]);
                n--;
            }
            SeatCore.Suffle(ref list);

            foreach(ChairModel chair in list)
            {
                while(true)
                {
                    n++;
                    if (_chairs[n].Checked)
                    {
                        _chairs[n] = chair;
                        break;
                    }
                }
            }
        }

        private void ChangeSize()
        {
            _nowrow = 0;
            _chairs.Clear();
            while(_chairs.Count < _room.Width*_room.Height)
            {
                if (_nowcol + 1 > _room.Width) { _nowrow++; _nowcol = 0; }
                else _nowcol++;

                _chairs.Add(new ChairModel(_nowrow, _nowcol, _inputname));
            }
                
        }

        public ObservableCollection<ChairModel> Chairs { get { return _chairs; } set { SetProperty(ref _chairs, value); } }
        public string InputName { get { return _inputname; } set { SetProperty(ref _inputname, value); } }
        public ClassRoom Room { get { ChangeSize(); return _room; } set { SetProperty(ref _room, value); } }
    }
}
