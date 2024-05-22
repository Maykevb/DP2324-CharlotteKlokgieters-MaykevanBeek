using Sudoku.models.BoardComponent;
using System.Text;

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

			DrawSeparator(rowLength, 0);

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

				if ((i + 1) % rowLength == 0 && (i + 1) % (squareHeight * rowLength) == 0 && i == board.Cells.Count - 1) 
				{
					DrawSeparator(rowLength, 2);
				}
				else if ((i + 1) % rowLength == 0 && (i + 1) % (squareHeight * rowLength) == 0)
				{
					DrawSeparator(rowLength, 1);
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

		public void DrawSeparator(double rowLength, int place) 
		{
            char startChar, endChar;
            switch (place)
            {
                case (int)SeparatorType.TOP:
                    startChar = '┌';
                    endChar = '┐';
                    break;
                case (int)SeparatorType.BOTTOM:
                    startChar = '└';
                    endChar = '┘';
                    break;
                case (int) SeparatorType.MIDDLE:
                default:
                    startChar = '├';
                    endChar = '┤';
                    break;
                
            }

            StringBuilder rowSeparator = new StringBuilder();
			rowSeparator.AppendLine();
            rowSeparator.Append(startChar);
            rowSeparator.Append(new string('-', (int)(rowLength + (rowLength % 2 + 1))));
            rowSeparator.Append(endChar);

            Console.WriteLine(rowSeparator.ToString());
        }
	}
}
