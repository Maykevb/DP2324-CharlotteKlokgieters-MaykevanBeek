using Sudoku.models.BoardComponent;
using System.Text;

namespace Sudoku.renderers
{
    public class SamuraiRenderer : iBoardRenderer
    {		
		private static readonly int ROW_LENGTH = 9; //One sudoku row		
		private static readonly int TWO_ROWS = ROW_LENGTH * 2; //Two sudoku rows
		private static readonly int	TWO_ROWS_WITH_SPACE = TWO_ROWS + 3; //Two sudoku rows + 3 spaces (in the middle)
		private static readonly int SIDE_SPACE = 8; //The amount of spaces on the side of the middle of the middle sudoku

		private static readonly int START_MID_SUDOKU = TWO_ROWS * 6; //At what point the row with the middle sudoku starts
		private static readonly int END_SIDE_SUDOKUS = START_MID_SUDOKU + TWO_ROWS_WITH_SPACE * 3; //At what point the side sudokus have ended and you only have to print the middle sudoku
		private static readonly int START_SIDE_SUDOKUS = END_SIDE_SUDOKUS + ROW_LENGTH * 3; //At what point the side sudokus start again, while you also still have the middle sudoku that has to be printed
		private static readonly int END_MID_SUDOKU = START_SIDE_SUDOKUS + TWO_ROWS_WITH_SPACE * 3; //At what point the rows with the middles sudoku have completely ended and you only have to print the last of the side sudokus

		private int row_counter = 0;

		public object Clone()
        {
            return new SamuraiRenderer();
        }

		public void DrawBoard(SudokuBoard board, int squareLength, int squareHeight)
		{
			DrawSeparator(ROW_LENGTH, squareLength, true);

			for (int i = 0; i < board.Cells.Count; i++)
			{
				DrawOneCharacter(board, i, squareLength, squareHeight);

				if (((i + 1) % (TWO_ROWS * squareLength) == 0 && (i < START_MID_SUDOKU)) && ((i + 1) % START_MID_SUDOKU == 0)) 
				{
					DrawSeparator(TWO_ROWS_WITH_SPACE, squareLength, false);
					continue;
				}
				
				if (((i + 1) % (TWO_ROWS * squareLength) == 0 && i < START_MID_SUDOKU) || 
					((i - END_MID_SUDOKU + 1) % (TWO_ROWS * squareLength) == 0 && i > END_MID_SUDOKU))
				{
					DrawSeparator(ROW_LENGTH, squareLength, true); 
					continue;
				}
				
				if (((i + 1) % TWO_ROWS == 0 && (i + 1) != (START_MID_SUDOKU + TWO_ROWS))&& !(i >= START_MID_SUDOKU && i <= END_MID_SUDOKU))
				{
					HandleGridLine(i, squareLength, '<');
					continue;
                }
				
				if ((i + 1) % ROW_LENGTH == 0 && (i + 1) != (START_MID_SUDOKU + ROW_LENGTH) &&(i + 1) != (START_MID_SUDOKU + TWO_ROWS) && (i < START_MID_SUDOKU || i > END_MID_SUDOKU)) 
				{
                    HandleGridLine(i, squareLength, '>');
                }
			}
		}

        private void HandleGridLine(int i, int squareLength, char type)
        {
            if ((type == '<' && i < END_MID_SUDOKU) || (type == '>' && i > END_MID_SUDOKU))
            {
                Console.WriteLine();
                row_counter++;
            }
            else
            {
                DrawEmptyRow(squareLength);
            }
        }

        private void DrawSeparator(double rowLength, int squareLength, bool middleEmpty)
		{
			row_counter++;

			StringBuilder rowSeparator = new StringBuilder();
			rowSeparator.AppendLine();
			rowSeparator.Append(new string('-', (int)(rowLength + (rowLength / squareLength) + 1))); 

			if (middleEmpty)
			{
				rowSeparator.Append(new string(' ', squareLength));
				rowSeparator.Append(new string('-', (int)(rowLength + (rowLength / squareLength) + 1))); 
			}
			
			Console.WriteLine(rowSeparator.ToString());
		}

        private void DrawLineRow(int length) 
		{
			for (int j = 0; j < length; j++)
			{
				Console.Write("-");
			}
		}

		private void DrawEmptyRow(int length)
		{
			for (int j = 0; j < length; j++)
			{
				Console.Write(" "); 
			}
		}

        private void DrawCell(int value)
		{
			Console.Write(value == 0 ? " " : value.ToString()); 
		}

        private void DrawOneCharacter(SudokuBoard board, int i, int squareLength, int squareHeight)
        {
            DrawEmptyRowIfNeeded(i);
            DrawVerticalSeparatorStart(i, squareLength);
            DrawCell(board.Cells[i].Value);
            DrawVerticalSeperatorMidOrEnd(i, squareLength);
            MoveToNextRowIfNeeded(i, squareLength);
        }

        private void DrawEmptyRowIfNeeded(int i)
        {
            if (row_counter >= 10 && row_counter <= 12 && (i % ROW_LENGTH == 0 || i % TWO_ROWS == 0)) //TODO static ints
            {
                DrawEmptyRow(SIDE_SPACE);
            }
        }

        private void DrawVerticalSeparatorStart(int i, int squareLength)
        {
            if (((i < START_MID_SUDOKU || i > END_MID_SUDOKU) && (i % ROW_LENGTH == 0 || i % TWO_ROWS == 0)) || ((i >= START_MID_SUDOKU && i <= END_MID_SUDOKU) && (((i - START_MID_SUDOKU) % TWO_ROWS_WITH_SPACE == 0 && i < END_SIDE_SUDOKUS) || (i >= END_SIDE_SUDOKUS && i <= START_SIDE_SUDOKUS && (i - END_SIDE_SUDOKUS) % ROW_LENGTH == 0))) || ((i >= START_SIDE_SUDOKUS && i <= END_MID_SUDOKU) && ((i - START_SIDE_SUDOKUS) % TWO_ROWS_WITH_SPACE == 0)))
            {
                Console.Write("|");
            }
        }

        private void DrawVerticalSeperatorMidOrEnd(int i, int squareLength)
        {
            if (((i < START_MID_SUDOKU || i > END_MID_SUDOKU) && ((i + 1) % squareLength == 0 && (i + 1) != (START_MID_SUDOKU + TWO_ROWS))) || ((i >= START_MID_SUDOKU && i <= END_MID_SUDOKU) && ((i + 1 - START_MID_SUDOKU) % squareLength == 0)))
            {
                Console.Write("|");
            }
        }

        private void MoveToNextRowIfNeeded(int i, int squareLength)
        {
            if (((i + 1) % END_MID_SUDOKU == 0) || (((i + 1 >= END_SIDE_SUDOKUS && i + 1 <= START_SIDE_SUDOKUS) && (i + 1 - END_SIDE_SUDOKUS) % ROW_LENGTH == 0) && (row_counter == 9 || row_counter == 12)))
            {
                DrawSeparator(TWO_ROWS_WITH_SPACE, squareLength, false);
                return;
            }

            if (((i + 1 >= END_SIDE_SUDOKUS && i + 1 <= START_SIDE_SUDOKUS) && (i + 1 - END_SIDE_SUDOKUS) % ROW_LENGTH == 0) || ((i >= START_MID_SUDOKU && i <= END_SIDE_SUDOKUS) && (i + 1 - START_MID_SUDOKU) % TWO_ROWS_WITH_SPACE == 0) || ((i >= START_SIDE_SUDOKUS && i <= END_MID_SUDOKU) && (i + 1 - START_SIDE_SUDOKUS) % TWO_ROWS_WITH_SPACE == 0 && (i + 1) % END_MID_SUDOKU != 0))
            {
                Console.WriteLine();
                row_counter++;
            }
        }
    }
}
