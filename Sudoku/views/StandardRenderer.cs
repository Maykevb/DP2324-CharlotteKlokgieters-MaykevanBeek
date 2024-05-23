using Sudoku.models.BoardComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.renderers
{
    public class StandardRenderer : iBoardRenderer
    {
        public object Clone()
        {
            return new StandardRenderer();
        }

		public void DrawBoard(SudokuBoard board, int squareLength, int squareHeight)
		{
			double rowLength = Math.Sqrt(board.Cells.Count);

			DrawHorizontalSeparator(rowLength, squareLength);

			for (int i = 0; i < board.Cells.Count; i++)
			{
				if (i % rowLength == 0)
				{
					DrawVerticalSeperator();
                }

				DrawCell(board.Cells[i].Value);

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

		public void DrawVerticalSeperator()
		{
            Console.Write("|");
        }

		public void DrawCell(int value)
		{
			Console.Write(value == 0 ? " " : value.ToString());
		}

		public void DrawHorizontalSeparator(double rowLength, int squareLength) 
		{
			Console.WriteLine("\n" + new string('-', (int)(rowLength + (rowLength / squareLength) + 1)));
		}
	}
}
