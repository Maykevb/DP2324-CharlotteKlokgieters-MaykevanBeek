using Sudoku.models.SudokuComponent;
using Sudoku.views;

namespace Sudoku.renderers
{
    public class SamuraiRenderer : AbstractRenderer
    {		
		private static readonly int NON_MID_ROWS = 6; //Amount of sudoku rows that don't have the middle sudoku in between
		private static readonly int MID_ONLY_ROWS = 3; //Amount of sudoku rows that only have the middle sudoku
		private static readonly int SIDE_SPACE = 8; //The amount of spaces on the side of the middle sudoku

		public override object Clone()
        {
            return new SamuraiRenderer();
        }

		//board[0] = upper left, board[1] = upper right, board[2] = middle, board[3] = lower left, board[4] = lower right
		public override void DrawBoard(SudokuGroup board, int squareLength, int squareHeight)
		{
			int rowLength = Convert.ToInt32(Math.Sqrt(board.Components[0].Components.Count));

			DrawSeperator(rowLength, squareLength, true);

            //Draw the first 9 rows -> entire upper sudokus and a little of the middle
            DrawSection(board, squareLength, rowLength, 0, 1, 2, true);

            //Draw the middle of the middle sudoku
            for (int j = 0; j < MID_ONLY_ROWS; j++)
			{
				DrawEmptyRow(SIDE_SPACE); 
				DrawVerticalSeperator();

				for (int i = 0; i < rowLength; i++)
				{				
					DrawCell(board.Components[2].Components[i + (rowLength * MID_ONLY_ROWS) + (rowLength * j)].Value);
					DrawSquareSeparator(i, squareLength);
				}

				if ((j + 1) == squareLength) 
				{
					DrawSeperator(rowLength, squareLength, false);
					continue;
				}
				
				DrawLine();
			}

            //Draw the last 9 rows -> entire lower sudokus and a little of the middle
            DrawSection(board, squareLength, rowLength, 3, 4, 2, false);
        }

        private void DrawSection(SudokuGroup board, int squareLength, int rowLength, int leftSudokuIndex, int rightSudokuIndex, int middleSudokuIndex, bool isUpper)
        {
            for (int j = 0; j < rowLength; j++)
            {
                // Draw left sudoku
                DrawVerticalSeperator();
                for (int i = 0; i < rowLength; i++)
                {
                    DrawCell(board.Components[leftSudokuIndex].Components[i + (rowLength * j)].Value);
                    DrawSquareSeparator(i, squareLength);
                }

                // Draw middle sudoku
                if ((isUpper && j < NON_MID_ROWS) || (!isUpper && j >= MID_ONLY_ROWS))
                {
                    DrawEmptyRow(squareLength);
                }
                else
                {
                    for (int i = squareLength; i < (squareLength * 2); i++)
                    {
                        int indexOffset = isUpper ? 0 : (rowLength * NON_MID_ROWS);
                        DrawCell(board.Components[middleSudokuIndex].Components[i + (rowLength * (j - (squareLength * 2))) + indexOffset].Value);
                    }
                }

                // Draw right sudoku
                DrawVerticalSeperator();
                for (int i = 0; i < rowLength; i++)
                {
                    DrawCell(board.Components[rightSudokuIndex].Components[i + (rowLength * j)].Value);
                    DrawSquareSeparator(i, squareLength);
                }

                // Draw row separators
                if ((j + 1) % squareLength != 0)
                {
                    DrawLine();
                    continue;
                }

                if ((j + 1) == squareLength)
                {
                    DrawSeperator(rowLength, squareLength, true);
                    continue;
                }

                DrawSeperator(rowLength, squareLength, false);
            }
        }

        private void DrawSeperator(int rowLength, int squareLength, bool middleEmpty)
		{
			if (middleEmpty)
			{
				Console.Write("\n" + new string('-', rowLength + (rowLength / squareLength) + 1) +
					new string(' ', squareLength) + new string('-', rowLength + (rowLength / squareLength) + 1) + "\n");
				return;
			} 

			Console.Write("\n" + new string('-', (rowLength * squareLength + 2)) + "\n");
		}


		private void DrawEmptyRow(int length)
		{
			for (int j = 0; j < length; j++)
			{
				Console.Write(" "); 
			}
		}

		private void DrawSquareSeparator(int i, int squareLength)
		{
			if ((i + 1) % squareLength == 0)
			{
				DrawVerticalSeperator();
			}
		}
    }
}
