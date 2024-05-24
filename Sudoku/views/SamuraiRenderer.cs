using Sudoku.models.BoardComponent;
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

		// board[0] = upper left, board[1] = upper right, board[2] = middle, board[3] = lower left, board[4] = lower right
		public void DrawBoard(SudokuGroup board, int squareLength, int squareHeight)
		{
			int rowLength = Convert.ToInt32(Math.Sqrt(board.Components[0].Components.Count));

			DrawSeparator(rowLength, squareLength, true);		
			
			for (int j = 0; j < rowLength; j++)
			{
				DrawSeparator();
				for (int i = 0; i < rowLength; i++)
				{
					DrawCell(board.Components[0].Components[i + (rowLength * j)].Value);
					DrawSquareSeparator(i, squareLength);
				}

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

				DrawSeparator();
				for (int i = 0; i < rowLength; i++)
				{
					DrawCell(board.Components[1].Components[i + (rowLength * j)].Value);
					DrawSquareSeparator(i, squareLength);
				}

				if ((j + 1) % squareLength != 0) 
				{
					DrawLine();
				}
				else if ((j + 1) == squareLength)
				{
					DrawSeparator(rowLength, squareLength, true);
				}
				else
				{
					DrawSeparator(rowLength, squareLength, false);
				}
			}
	
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
				}
				else
				{
					DrawLine();
				}
			}

			for (int j = 0; j < rowLength; j++)
			{
				DrawSeparator();
				for (int i = 0; i < rowLength; i++)
				{
					DrawCell(board.Components[3].Components[i + (rowLength * j)].Value);
					DrawSquareSeparator(i, squareLength);
				}

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

				DrawSeparator();
				for (int i = 0; i < rowLength; i++)
				{
					DrawCell(board.Components[4].Components[i + (rowLength * j)].Value);
					DrawSquareSeparator(i, squareLength);
				}

				if ((j + 1) % squareLength != 0) 
				{
					DrawLine();
				}
				else if ((j + 1) == squareLength) 
				{
					DrawSeparator(rowLength, squareLength, false);
				}
				else
				{
					DrawSeparator(rowLength, squareLength, true);
				}
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
