using Sudoku.models.BoardComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.models.states
{
    public interface iBoardState
    {
        public void Handle(SudokuBoard board);
        iBoardState goNext();
    }
}
