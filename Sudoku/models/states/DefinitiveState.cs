using Sudoku.models.BoardComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.models.states
{
    public class DefinitiveState : iBoardState
    {
        public void Handle(SudokuBoard board)
        {
            Console.WriteLine("Board is now in definitive state. You can now fill Sudoku cells.");
        }

        public iBoardState goNext()
        {
            return new CorrectionState();
        }
    }
}
