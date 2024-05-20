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

		public void drawBoard(SudokuBoard board, int squareLength, int squareHeight)
		{
			double rowLength = Math.Sqrt(board.Cells.Count);

			Console.WriteLine();
			writeRowSeperator(rowLength);

			for (int i = 0; i < board.Cells.Count; i++)
			{
				if (i % rowLength == 0)
				{
					Console.Write("|");
				}

				if (board.Cells[i].Value == 0)
				{
					Console.Write(" ");
				}
				else
				{
					Console.Write(board.Cells[i].Value);
				}

				if ((i + 1) % squareLength == 0 && !((i + 1) % rowLength == 0) || 
					(i + 1) % (squareHeight * squareLength) == 0)
				{
					Console.Write('|');
				}

				if ((i + 1) % rowLength == 0 && (i + 1) % (squareHeight * rowLength) == 0)
				{
					writeRowSeperator(rowLength);
				}
				else if ((i + 1) % rowLength == 0)
				{
					Console.WriteLine();
				}
				/*else if (i == board.Cells.Count - 1)
				{
					writeRowSeperator(rowLength);
				}*/
			}
		}

		public void writeRowSeperator(double rowLength)
		{
			Console.WriteLine();
			Console.Write('|');

			for (int j = 0; j < rowLength + (rowLength % 2 + 1); j++)
			{
				Console.Write("-");
			}

			Console.Write('|');
			Console.WriteLine();
		}
	}
}
