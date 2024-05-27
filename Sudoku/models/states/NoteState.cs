using Sudoku.models.SudokuComponent;

namespace Sudoku.models.states
{
    public class NoteState : iBoardState
    {
        public void PrintState()
        {
            string message = "Board is now in note state. You can now add, remove and see all notes." +
                "\n--> Press [/] to go to the correction state or press [+] to go to the definitive state";
            string line = new string('-', GameController.START_LINE_LENGTH);
            Console.WriteLine($"\n{line}\n{message}\n{line}");
        }
        public void DoAction(SudokuGroup board)
        {
            string message = "Place a note by typing row-column-value (seperated by -)";
            string line = new string('-', GameController.START_LINE_LENGTH);
            Console.WriteLine($"\n{line}\n{message}\n{line}");
            Console.ReadLine();
        }
    }
}
