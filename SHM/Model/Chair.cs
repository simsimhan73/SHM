using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHM.Model
{
    class Chair
    {
        private int _row;
        private int _col;
        private string _name;
        private string _note;

        public int Row { get { return _row; } set { _row = value; } }
        public int Col { get { return _col;} set { _col = value; } }
        public string Name { get { return _name;} set { _name = value; } }
        public string Note { get { return _note;} set { _note = value; } }
    }
}
