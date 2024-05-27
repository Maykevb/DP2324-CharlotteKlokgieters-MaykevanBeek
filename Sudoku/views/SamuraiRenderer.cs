using Sudoku.models.SudokuComponent;
using System.Text;

namespace Sudoku.renderers
{
    public class SamuraiRenderer : iBoardRenderer
    {		
		private static readonly int NON_MID_ROWS = 6; //Amount of sudoku rows that don't have the middle sudoku in between
		private static readonly int MID_ONLY_ROWS = 3; //Amount of sudoku rows that only have the middle sudoku
		private static readonly int SIDE_SPACE = 8; //The amount of spaces on the side of the middle sudoku

		public object Clone()
        {
            return new SamuraiRenderer();
        }

		//board[0] = upper left, board[1] = upper right, board[2] = middle, board[3] = lower left, board[4] = lower right
		public void DrawBoard(SudokuGroup board, int squareLength, int squareHeight)
		{
			int rowLength = Convert.ToInt32(Math.Sqrt(board.Components[0].Components.Count));

			DrawSeparator(rowLength, squareLength, true);		
			
			//Draw the first 9 rows -> entire upper sudokus and a little of the middle
			for (int j = 0; j < rowLength; j++)
			{
				//Draw upper left sudoku
				DrawSeparator();
				for (int i = 0; i < rowLength; i++)
				{
					DrawCell(board.Components[0].Components[i + (rowLength * j)].Value);
					DrawSquareSeparator(i, squareLength);
				}

				//Draw middle sudoku
				if (j < NON_MID_ROWS)
				{
					DrawEmptyRow(squareLength);
				}
				else
				{
					for (int i = squareLength; i < (squareLength * 2); i++) 
					{					
						DrawCell(board.Components[2].Components[i + (rowLength * (j - (squareLength * 2)))].Value);
					}
				}

				//Draw upper right sudoku
				DrawSeparator();
				for (int i = 0; i < rowLength; i++)
				{
					DrawCell(board.Components[1].Components[i + (rowLength * j)].Value);
					DrawSquareSeparator(i, squareLength);
				}

				//Draw row separators
				if ((j + 1) % squareLength != 0) 
				{
					DrawLine();
					continue;
				}

				if ((j + 1) == squareLength)
				{
					DrawSeparator(rowLength, squareLength, true);
					continue;
				}
				
				DrawSeparator(rowLength, squareLength, false);
			}

			//Draw the middle of the middle sudoku
			for (int j = 0; j < MID_ONLY_ROWS; j++)
			{
				DrawEmptyRow(SIDE_SPACE); 
				DrawSeparator();

				for (int i = 0; i < rowLength; i++)
				{				
					DrawCell(board.Components[2].Components[i + (rowLength * MID_ONLY_ROWS) + (rowLength * j)].Value);
					DrawSquareSeparator(i, squareLength);
				}

				if ((j + 1) == squareLength) 
				{
					DrawSeparator(rowLength, squareLength, false);
					continue;
				}
				
				DrawLine();
			}

			//Draw the last 9 rows -> entire lower sudokus and a little of the middle
			for (int j = 0; j < rowLength; j++)
			{
				//Draw lower left sudoku
				DrawSeparator();
				for (int i = 0; i < rowLength; i++)
				{
					DrawCell(board.Components[3].Components[i + (rowLength * j)].Value);
					DrawSquareSeparator(i, squareLength);
				}

				//Draw middle sudoku
				if (j >= MID_ONLY_ROWS)
				{
					DrawEmptyRow(squareLength);
				}
				else
				{
					for (int i = squareLength; i < (squareLength * 2); i++) 
					{
						DrawCell(board.Components[2].Components[i + (rowLength * NON_MID_ROWS) + (rowLength * j)].Value);
					}
				}

				//Draw lower right sudoku
				DrawSeparator();
				for (int i = 0; i < rowLength; i++)
				{
					DrawCell(board.Components[4].Components[i + (rowLength * j)].Value);
					DrawSquareSeparator(i, squareLength);
				}

				//Draw row separators
				if ((j + 1) % squareLength != 0) 
				{
					DrawLine();
					continue;
				}
				
				if ((j + 1) == squareLength) 
				{
					DrawSeparator(rowLength, squareLength, false);
					continue;
				}
				
				DrawSeparator(rowLength, squareLength, true);
			}
		}

        private void DrawSeparator(int rowLength, int squareLength, bool middleEmpty)
		{
			StringBuilder rowSeparator = new StringBuilder();
			rowSeparator.AppendLine();
			
			if (middleEmpty)
			{
				rowSeparator.Append(new string('█', (rowLength + (rowLength / squareLength) + 1)));
				rowSeparator.Append(new string(' ', squareLength));
				rowSeparator.Append(new string('█', (rowLength + (rowLength / squareLength) + 1))); 
			} 
			else
			{
				rowSeparator.Append(new string('█', (rowLength * squareLength + 2)));
			}
			
			Console.WriteLine(rowSeparator.ToString());
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

		private void DrawSquareSeparator(int i, int squareLength)
		{
			if ((i + 1) % squareLength == 0)
			{
				DrawSeparator();
			}
		}

		private void DrawSeparator()
		{
			Console.Write("█");
		}

		private void DrawLine()
		{
			Console.WriteLine();
		}
    }
}
