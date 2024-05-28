using Sudoku.models.SudokuComponent;

namespace Sudoku.models.states
{
    public class NoteState : AbstractState
    {
        public override void PrintState()
        {
            string message = "Board is now in note state. You can now add, remove and see all notes." +
                "\n--> Press [/] to go to the correction state or press [+] to go to the definitive state";
            string line = new string('-', GameController.START_LINE_LENGTH);
            Console.WriteLine($"\n{line}\n{message}\n{line}");
        }

        public override void DoAction(SudokuGroup board)
        {
            string message = "Place a note by typing row-column-value (seperated by -)";
            string line = new string('-', GameController.START_LINE_LENGTH);
            Console.WriteLine($"\n{line}\n{message}\n{line}");

            ReadInput(board);
        }

        public override void ReadInput(SudokuGroup board)
        {
            string input = Console.ReadLine() ?? "";

            if (!CheckState(input, board))
            {
                string[] parts = input.Split('-');
                int boardSize = board.Type == SudokuType.SAMURAI ? 21 : (int)Math.Sqrt(board.Components.Count);

                CheckInput(parts, board, boardSize, data =>
                {
                    return board.Type == SudokuType.SAMURAI
                        ? board.FillSamuraiCell(data[0], data[1], data[2])
                        : board.AddNormalNote(data[0], data[1], data[2]);
                });
            }
        }
    }
}
