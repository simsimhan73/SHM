using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHM.Model
{
    public class ChairModel
    {
        private int _row;
        private int _col;
        private string _name;
        private string _note = "";
        private bool _checked = false;

        public int Row { get { return _row; } set { _row = value; } }
        public int Col { get { return _col;} set { _col = value; } }
        public string Name { get { return _name;} set { _name = value; } }
        public string Note { get { return _note;} set { _note = value; } }
        public bool Checked { get { return _checked; } set { _checked = value; } }

        public ChairModel(int row, int col, string name, string note = "",bool check = false)
        {
            _row = row;
            _col = col;
            _name = name;
            _note = note;
            _checked = check;
        }
    }
}
