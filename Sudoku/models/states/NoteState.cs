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
            string message = "Board is now in note state. You can now add, remove and see all notes.";
            string line = new string('-', 70);
            Console.WriteLine($"\n{line}\n{message}\n{line}");
        }

        public iBoardState goNext()
        {
            return new DefinitiveState();
        }
    }
}
