﻿using Sudoku.models.BoardComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Sudoku.renderers
{
    public class SamuraiRenderer : iBoardRenderer
    {
		private static readonly int ROW_LENGTH = 9;
		private static readonly int TWO_ROWS = ROW_LENGTH * 2; //18
		private static readonly int	THREE_ROWS_NO_SPACE = TWO_ROWS + 3; //21
		private static readonly int ROW_LENGTH_WITH_SEPERATORS = ROW_LENGTH + 2; //11
		private static readonly int WRITE_LINE_LENGTH = 33;
		private static readonly int SIDE_SPACE = 8;

		private static readonly int START_MID_SUDOKU = TWO_ROWS * 6; //108
		private static readonly int END_SIDE_SUDOKUS = START_MID_SUDOKU + THREE_ROWS_NO_SPACE * 3; //
		private static readonly int START_SIDE_SUDOKUS = END_SIDE_SUDOKUS + ROW_LENGTH * 3; //
		private static readonly int END_MID_SUDOKU = START_SIDE_SUDOKUS + THREE_ROWS_NO_SPACE * 3; //

		private int row_counter = 0;

		public object Clone()
        {
            return new SamuraiRenderer();
        }

		public void DrawBoard(SudokuBoard board, int squareLength, int squareHeight)
		{
			Console.WriteLine();
			DrawSeparator(ROW_LENGTH, squareLength, true);

			for (int i = 0; i < board.Cells.Count; i++)
			{
				DrawOneCharacter(board, i, squareLength, squareHeight);

				if (((i + 1) % (TWO_ROWS * squareLength) == 0 && (i < START_MID_SUDOKU || i > END_MID_SUDOKU)) &&
					((i + 1) % START_MID_SUDOKU == 0))
				{
					DrawSeparator(THREE_ROWS_NO_SPACE, squareLength, false);
				}
				else
				if (((i + 1) % (TWO_ROWS * squareLength) == 0 && (i < START_MID_SUDOKU || i > END_MID_SUDOKU)))
				{
					DrawSeparator(ROW_LENGTH, squareLength, true); 
				}
				else
				if (((i + 1) % TWO_ROWS == 0 && (i + 1) != (START_MID_SUDOKU + TWO_ROWS))
					&& !(i >= START_MID_SUDOKU && i <= END_MID_SUDOKU)
					)
				{
					Console.WriteLine(); 
					row_counter++;
				}
				else if ((i + 1) % ROW_LENGTH == 0 && (i + 1) != (START_MID_SUDOKU + ROW_LENGTH) &&
					(i + 1) != (START_MID_SUDOKU + TWO_ROWS) &&
					(i < START_MID_SUDOKU || i > END_MID_SUDOKU)) // i + 1 ?
				{
					DrawEmptyRow(squareLength);
				}
			}

			DrawSeparator(ROW_LENGTH, squareLength, true);
		}

		public void DrawSeparator(double rowLength, int squareLength, bool middleEmpty)
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

		public void DrawLineRow(int length) 
		{
			for (int j = 0; j < length; j++)
			{
				Console.Write("-");
			}
		}

		public void DrawEmptyRow(int length)
		{
			for (int j = 0; j < length; j++)
			{
				Console.Write(" "); 
			}
		}

		public void DrawCell(int value)
		{
			Console.Write(value == 0 ? "0" : value.ToString()); //todo 0 to space
		}

		public void DrawOneCharacter(SudokuBoard board, int i, int squareLength, int squareHeight)
		{
			if (row_counter >= 10 && row_counter <= 12 && (i % ROW_LENGTH == 0 || i % TWO_ROWS == 0))
			{
				DrawEmptyRow(8); // todo static int
			}

			if ((i < START_MID_SUDOKU || i > END_MID_SUDOKU) && (i % ROW_LENGTH == 0 || i % TWO_ROWS == 0))
			{
				Console.Write("|");
			}
			else if ((i >= START_MID_SUDOKU && i <= END_MID_SUDOKU) && 
				(((i - START_MID_SUDOKU) % THREE_ROWS_NO_SPACE == 0 && i < END_SIDE_SUDOKUS) ||
				(i >= END_SIDE_SUDOKUS && i <= START_SIDE_SUDOKUS && (i - END_SIDE_SUDOKUS) % ROW_LENGTH == 0)))
			{
				Console.Write("|");
			}
			/*else if ((i + 1 >= END_SIDE_SUDOKUS && i + 1 <= START_SIDE_SUDOKUS) && 
				(i - END_SIDE_SUDOKUS % ROW_LENGTH == 0 || i - END_SIDE_SUDOKUS % TWO_ROWS == 0))
			{
				Console.Write(";|");
			}*/

			DrawCell(board.Cells[i].Value);

			if ((i < START_MID_SUDOKU || i > END_MID_SUDOKU) && 
				((i + 1) % squareLength == 0 && (i + 1) != (START_MID_SUDOKU + TWO_ROWS)))
			{
				Console.Write("|");
			}
			else 
			if ((i >= START_MID_SUDOKU && i <= END_MID_SUDOKU) && ((i + 1 - START_MID_SUDOKU) % squareLength == 0))
			{
				Console.Write("|");
			}

			if ((i + 1 >= END_SIDE_SUDOKUS && i + 1 <= START_SIDE_SUDOKUS) && (i + 1 - END_SIDE_SUDOKUS) % ROW_LENGTH == 0)
			{
				if (row_counter == 9 || row_counter == 12) // todo static int
				{
					DrawSeparator(THREE_ROWS_NO_SPACE, squareLength, false);
				}
				else
				{
					Console.WriteLine();
					row_counter++;
				}
			}
			/*else if ((i + 1 >= END_SIDE_SUDOKUS && i + 1 <= START_SIDE_SUDOKUS))
			{
				Console.WriteLine(i); //todo
				row_counter++;
			}*/

			// i + 1 ?
			else if ((i >= START_MID_SUDOKU && i <= END_SIDE_SUDOKUS) && 
				(i + 1 - START_MID_SUDOKU) % THREE_ROWS_NO_SPACE == 0)
			{
				Console.WriteLine(); 
				row_counter++;
			}
			else if ((i >= START_SIDE_SUDOKUS && i <= END_MID_SUDOKU) && 
				(i + 1 - START_SIDE_SUDOKUS) % THREE_ROWS_NO_SPACE == 0)
			{
				Console.WriteLine();
				row_counter++;
			}
		}
	}
}
