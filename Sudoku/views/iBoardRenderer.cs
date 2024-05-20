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
        public void drawBoard(SudokuBoard board, int squareLength, int squareHeight)
        {

        }

        public void clearBoard()
        {
            // TODO
        }
    }
}
