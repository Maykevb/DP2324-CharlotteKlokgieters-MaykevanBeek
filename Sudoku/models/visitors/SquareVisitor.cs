using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
	public class SquareVisitor : iBoardVisitor
	{
		public void VisitCell(SudokuCell cell, bool isCorrect)
		{
			cell.IsCorrect = isCorrect;
		}

		public void VisitBoard(SudokuGroup board)
		{
			int totalCells = board.Components.Count;
			int rowLength = Convert.ToInt32(Math.Sqrt(totalCells));
			int squareSize = Convert.ToInt32(Math.Sqrt(rowLength));
			int squareHeight = squareSize;
			int squareWidth = squareSize;

			// Adjust for 6x6 Sudoku if necessary
			if (board.Type == SudokuType.SIX_BY_SIX)
			{
				squareHeight = 2;
				squareWidth = 3;
			}

			int blocksPerRow = rowLength / squareHeight; //TODO switch usage of length vs height with \/ ??
			int blocksPerCol = rowLength / squareWidth;
			bool setToFalse = false;

			for (int blockRow = 0; blockRow < blocksPerRow; blockRow++)
			{
				for (int blockCol = 0; blockCol < blocksPerCol; blockCol++)
				{
					int baseRowIndex = blockRow * squareHeight;
					int baseColIndex = blockCol * squareWidth;
					setToFalse = false; //TODO right place ???

					for (int i = 0; i < squareHeight; i++)
					{
						for (int j = 0; j < squareWidth; j++)
						{
							int rowIndex = baseRowIndex + i;
							int colIndex = baseColIndex + j;
							int index = rowIndex * rowLength + colIndex;

							if (index < board.Components.Count && !board.Components[index].IsFixed && board.Components[index].Value != 0)
							{
								setToFalse = CheckSquare(board, rowLength, squareHeight, squareWidth, blockRow, blockCol, index, setToFalse);
							}
						}
					}
				}
			}
		}

		private bool CheckSquare(SudokuGroup board, int rowLength, int squareHeight, int squareWidth, int blockRow, int blockCol, int index, bool setToFalse)
		{
			int baseRowIndex = blockRow * squareHeight;
			int baseColIndex = blockCol * squareWidth;
			bool stf = setToFalse;

			for (int m = 0; m < squareHeight; m++)
			{
				for (int n = 0; n < squareWidth; n++)
				{
					int checkRowIndex = baseRowIndex + m;
					int checkColIndex = baseColIndex + n;
					int checkIndex = checkRowIndex * rowLength + checkColIndex;

					if (checkIndex < board.Components.Count && board.Components[index].Value == board.Components[checkIndex].Value && index != checkIndex)
					{
						VisitCell((SudokuCell) board.Components[index], false);
						stf = true;
						continue;
					}
					else if (!stf && !board.Components[index].IsCorrect && board.Components[index].Value != board.Components[checkIndex].Value && index != checkIndex)
					{
						VisitCell((SudokuCell) board.Components[index], true);
					}
				}
			}

			return stf;
		}
	}
}
