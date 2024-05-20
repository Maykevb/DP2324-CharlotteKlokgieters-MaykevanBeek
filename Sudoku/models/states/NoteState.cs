using Sudoku.models.BoardComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.models.states
{
    public class NoteState : iBoardState
    {
        public void Handle(SudokuBoard board)
        {
            Console.WriteLine("Board is now in note state. You can now add, remove and see all notes");
        }

        public iBoardState goNext()
        {
            return new DefinitiveState();
        }
    }
}
