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
        public void Handle()
        {
            string message = "Board is now in Correction state. You can now see which sudoku cells are filled correctly and which not." +
                "\n--> Press [+] to go to the definitive state or press [-] to go to the note state"; 
            string line = new string('-', 70);
            Console.WriteLine($"{line}\n{message}\n{line}");
        }

        public iBoardState goNext()
        {
            return new NoteState();
        }

        public void doAction(SudokuGroup board)
        {
            string message = "Green == correct | Red == incorrect";
            string line = new string('-', 70);
            Console.WriteLine($"\n{line}\n{message}\n{line}");
        }
    }
}
