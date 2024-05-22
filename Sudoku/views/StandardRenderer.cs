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

			DrawSeparator(rowLength, squareLength);

			for (int i = 0; i < board.Cells.Count; i++)
			{
				if (i % rowLength == 0)
				{
					Console.Write("|");
				}

				DrawCell(board.Cells[i].Value);

				if ((i + 1) % squareLength == 0 && !((i + 1) % rowLength == 0) || (i + 1) % (squareHeight * squareLength) == 0)
				{
					Console.Write('|');
				}

				if ((i + 1) % rowLength == 0 && (i + 1) % (squareHeight * rowLength) == 0) 
				{
					DrawSeparator(rowLength, squareLength);
				}
				else if ((i + 1) % rowLength == 0)
				{
					Console.WriteLine();
				}
			}
		}

		public void DrawCell(int value)
		{
			Console.Write(value == 0 ? " " : value.ToString());
		}

		public void DrawSeparator(double rowLength, int squareLength) 
		{
            StringBuilder rowSeparator = new StringBuilder();
			rowSeparator.AppendLine();
            rowSeparator.Append(new string('-', (int)(rowLength + (rowLength / squareLength) + 1))); 
            Console.WriteLine(rowSeparator.ToString());
        }
	}
}
