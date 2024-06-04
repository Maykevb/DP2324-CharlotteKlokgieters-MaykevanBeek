using Sudoku.models.SudokuComponent;
using Sudoku.renderers;

namespace Sudoku.models.states
{
    public class DefinitiveState : AbstractState
    {
        public override void PrintState()
        {
            string message = "Board is now in definitive state. You can fill Sudoku cells." + "\n--> Press [/] to go to the correction state or press [-] to go to the note state";
            string line = new string('-', GameController.START_LINE_LENGTH);

            Console.WriteLine($"\n{line}\n{message}\n{line}");
        }

        public override void DoAction(SudokuGroup board, GameController controller)
        {
            Console.WriteLine($"\n{new string('-', GameController.START_LINE_LENGTH)}\n" + $"Fill a cell by typing row-column-value (seperated by -)\n{new string('-', GameController.START_LINE_LENGTH)}");
            ReadInput(board, controller);
        }

        public override void ReadInput(SudokuGroup board, GameController controller)
        {
            string input = Console.ReadLine() ?? "";

            if (!CheckState(input, board, controller))
            {
                string[] parts = input.Split('-');
                int boardSize = board.Type == SudokuType.SAMURAI ? 21 : (int)Math.Sqrt(board.Components.Count);

                CheckInput(parts, board, boardSize, controller, data =>
                {
                    return board.Type == SudokuType.SAMURAI
                        ? board.HandleSamuraiCell(data[0], data[1], data[2], false)
                        : board.FillNormalCell(data[0], data[1], data[2]);
                });				
			}
        }

        public override void DisplayBoard(iBoardRenderer renderer, SudokuGroup board, int length, int height)
        {
            renderer.DrawBoard(board, length, height);
        }
    }
}
