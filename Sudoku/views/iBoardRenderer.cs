using Sudoku.models.BoardComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.renderers
{
    public interface iBoardRenderer : ICloneable
    {
        public void DrawBoard(SudokuBoard board, int squareLength, int squareHeight)
        {

        }

		public void DrawSeparator(double rowLength, int place)
        {

        }


		public void clearBoard()
        {
            // TODO
        }
    }
}
