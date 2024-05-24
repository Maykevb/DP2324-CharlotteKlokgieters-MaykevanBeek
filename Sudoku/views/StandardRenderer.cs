﻿using Sudoku.models.BoardComponent;

namespace Sudoku.renderers
{
    public class StandardRenderer : iBoardRenderer
    {
        public object Clone()
        {
            return new StandardRenderer();
        }

		public void DrawBoard(SudokuGroup board, int squareLength, int squareHeight)
		{
			int rowLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));

			DrawHorizontalSeparator(rowLength, squareLength);

			for (int i = 0; i < board.Components.Count; i++)
			{
				if (i % rowLength == 0)
				{
					DrawVerticalSeperator();
                }

				DrawCell(board.Components[i].Value);

				if ((i + 1) % squareLength == 0 && !((i + 1) % rowLength == 0) || (i + 1) % (squareHeight * squareLength) == 0)
				{
                    DrawVerticalSeperator();
                }

				if ((i + 1) % rowLength == 0 && (i + 1) % (squareHeight * rowLength) == 0)
				{
					DrawHorizontalSeparator(rowLength, squareLength);
					continue;
				}

				if ((i + 1) % rowLength == 0)
				{
					Console.WriteLine();
				}
			}
		}

		private void DrawVerticalSeperator()
		{
            Console.Write("█");
        }

		private void DrawCell(int value)
		{
			Console.Write(value == 0 ? " " : value.ToString());
		}

		private void DrawHorizontalSeparator(int rowLength, int squareLength) 
		{
			Console.WriteLine("\n" + new string('█', rowLength + (rowLength / squareLength) + 1));
		}
	}
}
