using Sudoku.models.BoardComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.models.states
{
    public class CorrectionState : iBoardState
    {
        public void Handle(SudokuBoard board)
        {
            Console.WriteLine("Board is now in Correction state. You can now see which sudoku cells are filled correctly and which not.");
            
        }

        public iBoardState goNext()
        {
            return new NoteState();
        }        
    }
}
