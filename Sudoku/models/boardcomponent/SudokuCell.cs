using Sudoku.models.visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.models.BoardComponent
{
    public class SudokuCell : iBoardComponent
    {
        private int value;
        private bool isFixed;
        private int[] notes = new int[9];

        public SudokuCell(int value, bool isFixed)
        {
            this.value = value;
            this.isFixed = isFixed;
        }

        public void Accept(iBoardVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public bool IsFixed
        {
            get { return isFixed; }
            set { isFixed = value; }
        }

        public int[] Notes
        {
            get { return notes; }
            set { notes = value; }
        }
    }
}
