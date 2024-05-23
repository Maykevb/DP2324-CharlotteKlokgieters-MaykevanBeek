using Sudoku.models.BoardComponent;

namespace Sudoku.renderers
{
    public class JigsawRenderer : iBoardRenderer
    {
        public object Clone()
        {
            return new JigsawRenderer();
        }

		public void DrawBoard(SudokuBoard board, int squareLength, int squareHeight)
        {
			double rowLength = Math.Sqrt(board.Cells.Count);

			DrawHorizontalSeparatorEdge(rowLength * 2, 1);

			for (int i = 0; i < board.Cells.Count; i++)
			{
				if ((i % rowLength == 0 || i % rowLength * 2 == 0))
				{
					Console.Write("|");
				}

				DrawCell(board.Cells[i].Value);

				if (i + 1 != board.Cells.Count && board.Cells[i].Block == board.Cells[i + 1].Block)
				{				
					Console.Write(" ");
				}
				else
				{
					Console.Write("|");
				}

				if ((i + 1) % rowLength == 0 && (i + 1) < board.Cells.Count)
				{
					DrawHorizontalSeparator(i, board, rowLength);
				}
			}

			DrawHorizontalSeparatorEdge(rowLength * 2, 1); 
		}

		private void DrawHorizontalSeparatorEdge(double rowLength, int extraSeparators)
		{
			Console.WriteLine("\n" + new string('-', (int)(rowLength + extraSeparators)));
		}

		private void DrawCell(int value)
		{
			Console.Write(value == 0 ? " " : value.ToString()); 
		}

		private void DrawHorizontalSeparator(int i, SudokuBoard board, double rowLength)
		{
			Console.WriteLine();
			Console.Write("|");

			bool lastNumbersAlsoInBlock = false;
			for (int j = Convert.ToInt32(rowLength); j > 0; j--)
			{
				if ((i + Convert.ToInt32(rowLength)) <= board.Cells.Count && board.Cells[i + 1 - j].Block == board.Cells[i + 1 + (Convert.ToInt32(rowLength) - j)].Block)
				{
					if (lastNumbersAlsoInBlock)
					{
						Console.Write(" ");
					}

					Console.Write(" ");
					lastNumbersAlsoInBlock = true;

					if (i + 1 != board.Cells.Count && ((i + 2 + (Convert.ToInt32(rowLength) - j)) < board.Cells.Count) && board.Cells[i + 1 + (Convert.ToInt32(rowLength) - j)].Block != board.Cells[i + 2 + (Convert.ToInt32(rowLength) - j)].Block && (i + 2 + (Convert.ToInt32(rowLength) - j)) % rowLength != 0)
					{
						Console.Write("|");
						lastNumbersAlsoInBlock = false;
					}

					if (i + 1 != board.Cells.Count && ((i + 2 + (Convert.ToInt32(rowLength) - j)) < board.Cells.Count) && board.Cells[i + 1 + (Convert.ToInt32(rowLength) - j)].Block == board.Cells[i + 2 + (Convert.ToInt32(rowLength) - j)].Block && board.Cells[i + 1 - j].Block != board.Cells[i + 2 - j].Block)
					{
						Console.Write("-");
					}
				}
				else
				{
					Console.Write("-");
					lastNumbersAlsoInBlock = false;

					if ((((i + 1 + (Convert.ToInt32(rowLength) - j)) % rowLength == 0) || ((i + 2 + (Convert.ToInt32(rowLength) - j)) % rowLength != 0)) && ((i + 1 + Convert.ToInt32(rowLength)) < board.Cells.Count && board.Cells[i + 1 + (Convert.ToInt32(rowLength) - j)].Block != board.Cells[i + 2 + (Convert.ToInt32(rowLength) - j)].Block))
					{
						Console.Write("|");
					}
					else if (!((i + 1 + Convert.ToInt32(rowLength)) < board.Cells.Count && board.Cells[i + 1 + (Convert.ToInt32(rowLength) - j)].Block != board.Cells[i + 2 + (Convert.ToInt32(rowLength) - j)].Block) && (i + 2 + (Convert.ToInt32(rowLength) - j) < board.Cells.Count))
					{
						Console.Write("-");
					}
				}
			}

			Console.WriteLine("|");
		}
	}
}
