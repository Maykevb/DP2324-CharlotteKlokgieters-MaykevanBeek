using Sudoku.models.BoardComponent;

namespace Sudoku.renderers
{
    public class JigsawRenderer : iBoardRenderer
    {
        public object Clone()
        {
            return new JigsawRenderer();
        }

		public void DrawBoard(SudokuGroup board, int squareLength, int squareHeight)
        {
			int rowLength = Convert.ToInt32(Math.Sqrt(board.Components.Count)); 

			DrawHorizontalSeparatorEdge(rowLength * 2, 1);

			for (int i = 0; i < board.Components.Count; i++)
			{
				if ((i % rowLength == 0 || i % rowLength * 2 == 0))
				{
					DrawSeperator();
				}

				DrawCell(board.Components[i].Value);

				if (i + 1 != board.Components.Count && board.Components[i].Block == board.Components[i + 1].Block)
				{
					DrawSpace();
				}
				else
				{
					DrawSeperator(); 
				}

				if ((i + 1) % rowLength == 0 && (i + 1) < board.Components.Count)
				{
					DrawHorizontalSeparatorLine(i, board, rowLength);
				}
			}

			DrawHorizontalSeparatorEdge(rowLength * 2, 1); 
		}

		private void DrawSeperator()
		{
			Console.Write("█");
		}

		private void DrawSpace()
		{
			Console.Write(" ");
		}

		private void StartNewLine()
		{
			Console.WriteLine();
		}

		private void DrawHorizontalSeparatorEdge(int rowLength, int extraSeparators)
		{
			Console.WriteLine("\n" + new string('█', rowLength + extraSeparators)); 
		}

		private void DrawCell(int value)
		{
			Console.Write(value == 0 ? " " : value.ToString()); 
		}

		private void DrawHorizontalSeparatorLine(int i, SudokuGroup board, int rowLength)
		{
			StartNewLine();
			DrawSeperator();

			bool lastNumbersAlsoInBlock = false;
			for (int j = rowLength; j > 0; j--)
			{
				if ((i + rowLength) <= board.Components.Count && IfBlockPrevRowIsSameAsThisRow(i, j, rowLength, board))
				{
					if (lastNumbersAlsoInBlock)
					{
						DrawSpace();
					}

					DrawSpace();
					lastNumbersAlsoInBlock = true;

					if (IfValidIndex(i, j, rowLength, board) && !IfBlockIsSame(i, j, rowLength, board) && (i + 2 + (rowLength - j)) % rowLength != 0)
					{
						DrawSeperator(); 
						lastNumbersAlsoInBlock = false;
						continue;
					}

					if (IfValidIndex(i, j, rowLength, board) && IfBlockIsSame(i, j, rowLength, board) && !IfBlockIsSameAsPrevRow(i, j, board))
					{
						DrawSeperator(); 
					}
				}
				else
				{
					DrawSeperator(); 
					lastNumbersAlsoInBlock = false;

					if (IfValuesAreInSameRow(i, j, rowLength) && (IfNotEndOfBoard(i, rowLength, board) && !IfBlockIsSame(i, j, rowLength, board)))
					{
						DrawSeperator(); 
						continue;
					}

					if (!(IfNotEndOfBoard(i, rowLength, board) && !IfBlockIsSame(i, j, rowLength, board)) && IfValidIndex(i, j, rowLength, board))
					{
						DrawSeperator();
					}
				}
			}

			DrawSeperator();
			StartNewLine();
		}

		private bool IfBlockPrevRowIsSameAsThisRow(int i, int j, int rowLength, SudokuGroup board)
		{
			return (board.Components[i + 1 - j].Block == board.Components[i + 1 + (rowLength - j)].Block);
		}

		private bool IfValuesAreInSameRow(int i, int j, int rowLength)
		{
			return (((i + 1 + (rowLength - j)) % rowLength == 0) || ((i + 2 + (rowLength - j)) % rowLength != 0));
		}

		private bool IfValidIndex(int i, int j, int rowLength, SudokuGroup board)
		{
			return (i + 1 != board.Components.Count && ((i + 2 + (rowLength - j)) < board.Components.Count));
		}

		private bool IfNotEndOfBoard(int i, int rowLength, SudokuGroup board)
		{
			return ((i + 1 + rowLength) < board.Components.Count);
		}

		private bool IfBlockIsSame(int i, int j, int rowLength, SudokuGroup board)
		{
			return (board.Components[i + 1 + (rowLength - j)].Block == board.Components[i + 2 + (rowLength - j)].Block);
		}

		private bool IfBlockIsSameAsPrevRow(int i, int j, SudokuGroup board)
		{
			return (board.Components[i + 1 - j].Block == board.Components[i + 2 - j].Block);
		}
	}
}
