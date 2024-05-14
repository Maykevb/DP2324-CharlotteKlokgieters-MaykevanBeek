using Sudoku.models.visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.models.BoardComponent
{
    public class SudokuBoard : iBoardComponent
    {
        private List<SudokuCell> cells = new List<SudokuCell>();

        public void Accept(iBoardVisitor visitor)
        {
            visitor.Visit(this);
        }

        public List<SudokuCell> Cells
        {
            get { return cells; }
            set { this.cells = value; }
        }
    }
}
