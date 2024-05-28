using Sudoku.models.SudokuComponent;

namespace Sudoku.models.states
{
    public abstract class AbstractState : iBoardState
    {
        public abstract void PrintState();

        public abstract void DoAction(SudokuGroup board);

        public abstract void ReadInput(SudokuGroup board);

        public bool CheckState(string input, SudokuGroup board)
        {
            if (input != null && (input != "/" && input != "-" && input != "+")) return false;

            if (input == "/")
            {
                board.SwitchState(new CorrectionState());
            }

            if (input == "-")
            {
                board.SwitchState(new NoteState());
            }

            if(input == "+")
            {
                board.SwitchState(new DefinitiveState());
            }

            board.State.PrintState();
            ReadInput(board);
            return true;
        }

        public int[] ValidateInput(string[] parts, SudokuGroup board, int boardSize)
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

            if (value > boardSize)
            {
                Console.WriteLine($"Invalid input. Value must be smaller than {boardSize}");
                ReadInput(board);
                return [];
            }

            if( value < 1 || value > 9)
            {
                Console.WriteLine($"Invalid input. Value must be between 1 and 9");
                ReadInput(board);
                return [];
            }

            return [row, col, value];
        }

        public void CheckLength(string[] parts, SudokuGroup board, int length)
        {
            if (parts.Length != length)
            {
                Console.WriteLine("Invalid input. Please enter row-column-value separated by '-'");
                ReadInput(board);
            }
        }

        public void CheckInput(string[] parts, SudokuGroup board, int boardSize, Func<int[], bool> fillCellFunc)
        {
            CheckLength(parts, board, 3);
            int[] data = ValidateInput(parts, board, boardSize);

            if (!fillCellFunc(data))
            {
                ReadInput(board);
            }
        }
    }
}
