using Sudoku.models.SudokuComponent;
using System.IO;

namespace Sudoku.models.states
{
    public class DefinitiveState : iBoardState
    {
        public void PrintState()
        {
            string message = "Board is now in definitive state. You can fill Sudoku cells." +
                "\n--> Press [/] to go to the correction state or press [-] to go to the note state";
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
                int boardSize = (int)Math.Sqrt(board.Components.Count);

                if (board.Type == SudokuType.SAMURAI)
                {
                    CheckSamuraiInput(parts, board, boardSize);
                    return;
                }

                CheckNumber(parts, board, boardSize);
            }
        }

        public Boolean CheckState(string input, SudokuGroup board)
        {
            if (input != null && (input.ToLower() == "/" || input == "-"))
            {
                
                if (input.ToLower() == "/")
                {
                    board.SwitchState(new CorrectionState());
                    board.State.PrintState();
                    return true;
                }
                
                if (input == "-")
                {
                    board.SwitchState(new NoteState());
                    board.State.PrintState();
                }

                ReadInput(board);
                return true;
            }
            return false;
        }

        public void CheckSamuraiInput(string[] parts, SudokuGroup board, int boardSize)
        {
            checkLength(parts, board, 3);
            int[] data = validateInput(parts, board, 21);

            if (!board.FillSamuraiCell(data[0], data[1], data[2]))
            {
                ReadInput(board);
            }
        }

        public void CheckNumber(string[] parts, SudokuGroup board, int boardSize)
        {
            checkLength(parts, board, 3);
            int[] data = validateInput(parts, board, boardSize);

            if (!board.FillCell(data[0], data[1], data[2]))
            {
                ReadInput(board);
            }
        }

        public int[] validateInput(string[] parts, SudokuGroup board, int boardSize)
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

        public void checkLength(string[] parts, SudokuGroup board, int length)
        {
            if (parts.Length != length)
            {
                Console.WriteLine("Invalid input. Please enter row-column-value separated by '-'");
                ReadInput(board);
            }
        }
    }
}
