using Sudoku.models.SudokuComponent;

namespace Sudoku.models.states
{
    public class CorrectionState : iBoardState
    {
        public void PrintState()
        {
            string message = "Board is now in Correction state. You can now see which sudoku cells are filled correctly and which not." +
                "\n--> Press [+] to go to the definitive state or press [-] to go to the note state"; 
            string line = new string('-', GameController.START_LINE_LENGTH);
            Console.WriteLine($"{line}\n{message}\n{line}");
        }

        public void DoAction(SudokuGroup board)
        {
            string message = "Green == correct | Red == incorrect";
            string line = new string('-', GameController.START_LINE_LENGTH);
            Console.WriteLine($"\n{line}\n{message}\n{line}");
        }
    }
}
