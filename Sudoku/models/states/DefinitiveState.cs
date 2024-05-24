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
        public void Handle()
        {
            string message = "Board is now in definitive state. You can fill Sudoku cells." +
                "\n--> Press [/] to go to the correction state or press [-] to go to the note state";
            string line = new string('-', 70);
            Console.WriteLine($"\n{line}\n{message}\n{line}");
        }

        public iBoardState goNext()
        {
            return new CorrectionState();
        }

        public void doAction(SudokuGroup board)
        {
            string message = "Fill a cell by typing row-column-value (seperated by -)";
            string line = new string('-', 70);
            Console.WriteLine($"\n{line}\n{message}\n{line}");
            FillCell(board);
        }

        public void FillCell(SudokuGroup board)
        {
            string input = Console.ReadLine() ?? "";

            CheckState(input, board);

        }

        public void CheckState(string input, SudokuGroup board)
        {
            if (input != null && (input.ToLower() == "/" || input == "-"))
            {
                
                if (input.ToLower() == "/")
                {
                    board.SwitchState(new CorrectionState());
                    board.State.Handle();
                }
                else if (input == "-")
                {
                    board.SwitchState(new NoteState());
                    board.State.Handle();
                }

                return;
            }
        }
    }
}
