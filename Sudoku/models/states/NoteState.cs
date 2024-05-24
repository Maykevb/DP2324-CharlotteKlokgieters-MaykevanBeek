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
        public void Handle()
        {
            string message = "Board is now in note state. You can now add, remove and see all notes." +
                "\n--> Press [/] to go to the correction state or press [+] to go to the definitive state";
            string line = new string('-', 70);
            Console.WriteLine($"\n{line}\n{message}\n{line}");
        }

        public iBoardState goNext()
        {
            return new DefinitiveState();
        }

        public void doAction(SudokuGroup board)
        {
            string message = "Place a note by typing row-column-value (seperated by -)";
            string line = new string('-', 70);
            Console.WriteLine($"\n{line}\n{message}\n{line}");
            Console.ReadLine();
        }
    }
}
