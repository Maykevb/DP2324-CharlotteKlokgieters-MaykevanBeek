using Sudoku.models.SudokuComponent;

namespace Sudoku.models.states
{
    public class DefinitiveState : iBoardState
    {
        public void PrintState()
        {
            string message = "Board is now in definitive state. You can fill Sudoku cells." + "\n--> Press [/] to go to the correction state or press [-] to go to the note state";
            string line = new string('-', GameController.START_LINE_LENGTH);

            Console.WriteLine($"\n{line}\n{message}\n{line}");
        }

        public void DoAction(SudokuGroup board)
        {
            Console.WriteLine($"\n{new string('-', GameController.START_LINE_LENGTH)}\n" + $"Fill a cell by typing row-column-value (seperated by -)\n{new string('-', GameController.START_LINE_LENGTH)}");
            ReadInput(board);
        }

        public void ReadInput(SudokuGroup board)
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
                        : board.FillNormalCell(data[0], data[1], data[2]);
                });
            }
        }

        private bool CheckState(string input, SudokuGroup board)
        {
            if (input != null && (input != "/" || input != "-")) return false;
                
            if (input == "/")
            {
                board.SwitchState(new CorrectionState());
            }
                
            if (input == "-")
            {
                board.SwitchState(new NoteState());
            }

            board.State.PrintState();
            ReadInput(board);
            return true;
        }

        private void CheckInput(string[] parts, SudokuGroup board, int boardSize, Func<int[], bool> fillCellFunc)
        {
            CheckLength(parts, board, 3);
            int[] data = ValidateInput(parts, board, boardSize);

            if (!fillCellFunc(data))
            {
                ReadInput(board);
            }
        }

        private int[] ValidateInput(string[] parts, SudokuGroup board, int boardSize)
        {
            if (!int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col) || !int.TryParse(parts[2], out int value))
            {
                Console.WriteLine("Invalid input. Please enter valid numbers for row, column, and value.");
                ReadInput(board);
                return [];
            }

            if (row < 1 || row > boardSize || col < 1 || col > boardSize)
            {
                Console.WriteLine("Invalid input. Row and column must be between 1 and " + boardSize);
                ReadInput(board);
                return [];
            }

            if (value < 1 || value > boardSize)
            {
                Console.WriteLine($"Invalid input. Value must be between 1 and {boardSize}");
                ReadInput(board);
                return [];
            }

            return [row, col, value];
        }

        private void CheckLength(string[] parts, SudokuGroup board, int length)
        {
            if (parts.Length != length)
            {
                Console.WriteLine("Invalid input. Please enter row-column-value separated by '-'");
                ReadInput(board);
            }
        }
    }
}
