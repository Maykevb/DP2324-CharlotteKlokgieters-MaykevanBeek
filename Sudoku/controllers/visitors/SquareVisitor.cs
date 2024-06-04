using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
	public class SquareVisitor : AbstractVisitor
	{
		public override void VisitBoard(SudokuGroup board, int boardIndex, SudokuGroup fullBoard)
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

			int blocksPerRow = rowLength / squareHeight; 
			int blocksPerCol = rowLength / squareWidth;

			for (int blockRow = 0; blockRow < blocksPerRow; blockRow++)
			{
				for (int blockCol = 0; blockCol < blocksPerCol; blockCol++)
				{
					int baseRowIndex = blockRow * squareHeight;
					int baseColIndex = blockCol * squareWidth;

					for (int i = 0; i < squareHeight; i++)
					{
						for (int j = 0; j < squareWidth; j++)
						{
							int rowIndex = baseRowIndex + i;
							int colIndex = baseColIndex + j;
							int index = rowIndex * rowLength + colIndex;

							if (index < board.Components.Count && !board.Components[index].IsFixed && board.Components[index].Value != 0)
							{
								CheckSquare(board, rowLength, squareHeight, squareWidth, blockRow, blockCol, index, boardIndex, fullBoard);
							}
						}
					}
				}
			}
		}

		private void CheckSquare(SudokuGroup board, int rowLength, int squareHeight, int squareWidth, int blockRow, int blockCol, int index, int boardIndex, 
			SudokuGroup fullBoard)
		{
			int baseRowIndex = blockRow * squareHeight;
			int baseColIndex = blockCol * squareWidth;

			for (int m = 0; m < squareHeight; m++)
			{
				for (int n = 0; n < squareWidth; n++)
				{
					int checkRowIndex = baseRowIndex + m;
					int checkColIndex = baseColIndex + n;
					int checkIndex = checkRowIndex * rowLength + checkColIndex;

					if (checkIndex < board.Components.Count && board.Components[index].Value == board.Components[checkIndex].Value && index != checkIndex)
					{
						board.Components[index].Accept(this, board, boardIndex, index, fullBoard);
					}
				}
			}
		}

		public override void VisitJigsaw(SudokuGroup board)
		{
			int totalCells = board.Components.Count;
			int sameVal = 0;

			for (int i = 0; i < totalCells; i++)
			{
				int block = (int) board.Components[i].Block;
				sameVal = 0;
				for (int j = 0; j < totalCells; j++)
				{
					if (i != j && board.Components[i].Block == board.Components[j].Block && board.Components[i].Value == board.Components[j].Value && 
						!board.Components[i].IsFixed)
					{
						sameVal++;
					}
				}

				if (sameVal > 0)
				{
					board.Components[i].Accept(this, board, -1, i, board);
				}
			}
		}
	}
}
