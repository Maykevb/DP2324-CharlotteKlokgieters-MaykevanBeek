using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
	public class SquareVisitor : iBoardVisitor
	{
		public void VisitCell(SudokuCell cell, bool isCorrect, SudokuGroup board, int boardIndex, int celIndex, SudokuGroup fullBoard)
		{
			if (boardIndex >= 0)
			{
				int totalCells = board.Components.Count; //81
				int rowLength = Convert.ToInt32(Math.Sqrt(totalCells)); //9
				int squareLength = Convert.ToInt32(Math.Sqrt(rowLength)); //3
				int index = -1;

				switch (boardIndex)
				{
					case 0: // Upper left
						if ((celIndex >= (rowLength * 6 + squareLength * 2) && celIndex < rowLength * 7) || 
							(celIndex >= (rowLength * 7 + squareLength * 2) && celIndex < rowLength * 8) || 
							(celIndex >= (rowLength * 8 + squareLength * 2) && celIndex < board.Components.Count)) // TODO 
						{
							index = celIndex - (rowLength * 6 + squareLength * 2);
							fullBoard.Components[2].Components[index].IsCorrect = isCorrect;
							break;
						}
						break;
					case 1: // Upper right
						if ((celIndex >= (rowLength * 6) && celIndex < (rowLength * 6 + squareLength)) || 
							(celIndex >= (rowLength * 7) && celIndex < (rowLength * 7 + squareLength)) || 
							(celIndex >= (rowLength * 8) && celIndex < (rowLength * 8 + squareLength))) // TODO 
						{
							index = celIndex - (rowLength * 6 + squareLength * 2);
							fullBoard.Components[2].Components[index].IsCorrect = isCorrect;
							break;
						}	
						break;
					case 2: // Middle
						if ((celIndex >= 0 && celIndex < squareLength) ||
							(celIndex >= rowLength && celIndex < (rowLength * 1 + squareLength)) ||
							(celIndex >= (rowLength * 2) && celIndex < (rowLength * 2 + squareLength))) // TODO 
						{
							index = celIndex + (rowLength * 6) + (squareLength * 2);
							fullBoard.Components[0].Components[index].IsCorrect = isCorrect;
							break;
						}

						if ((celIndex >= (squareLength * 2) && celIndex < rowLength * 1) ||
							(celIndex >= (rowLength + squareLength * 2) && celIndex < rowLength * 2) ||
							(celIndex >= (rowLength * 2 + squareLength * 2) && celIndex < (rowLength * 3))) // TODO 
						{
							index = celIndex + (rowLength * 6) - (squareLength * 2);
							fullBoard.Components[1].Components[index].IsCorrect = isCorrect;
							break;
						}

						if ((celIndex >= (rowLength * 6) && celIndex < (rowLength * 6 + squareLength)) ||
							(celIndex >= (rowLength * 7) && celIndex < (rowLength * 7 + squareLength)) ||
							(celIndex >= (rowLength * 8) && celIndex < (rowLength * 8 + squareLength))) // TODO 
						{
							index = celIndex - (rowLength * 6 + squareLength * 2);
							fullBoard.Components[3].Components[index].IsCorrect = isCorrect;
							break;
						}

						if ((celIndex >= (rowLength * 6 + squareLength * 2) && celIndex < rowLength * 7) ||
							(celIndex >= (rowLength * 7 + squareLength * 2) && celIndex < rowLength * 8) ||
							(celIndex >= (rowLength * 8 + squareLength * 2) && celIndex < board.Components.Count)) // TODO 
						{
							index = celIndex - (rowLength * 6 + squareLength * 2);
							fullBoard.Components[4].Components[index].IsCorrect = isCorrect;
							break;
						}
						break;
					case 3: // Lower left
						if ((celIndex >= (squareLength * 2) && celIndex < rowLength * 1) || 
							(celIndex >= (rowLength + squareLength * 2) && celIndex < rowLength * 2) || 
							(celIndex >= (rowLength * 2 + squareLength * 2) && celIndex < (rowLength * 3))) // TODO 
						{
							index = celIndex + (rowLength * 6) - (squareLength * 2);
							fullBoard.Components[2].Components[index].IsCorrect = isCorrect;
							break;
						}
						break;
					case 4: // Lower right
						if ((celIndex >= 0 && celIndex < squareLength) || 
							(celIndex >= rowLength && celIndex < (rowLength * 1 + squareLength)) || 
							(celIndex >= (rowLength * 2) && celIndex < (rowLength * 2 + squareLength))) // TODO 
						{
							index = celIndex + (rowLength * 6) + (squareLength * 2);
							fullBoard.Components[2].Components[index].IsCorrect = isCorrect;
							break;
						}
						break;
				}
			}
			
			cell.IsCorrect = isCorrect;		
		}

		public void VisitBoard(SudokuGroup board, int boardIndex, SudokuGroup fullBoard)
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
								setToFalse = CheckSquare(board, rowLength, squareHeight, squareWidth, blockRow, blockCol, index, setToFalse, boardIndex, fullBoard);
							}
						}
					}
				}
			}
		}

		private bool CheckSquare(SudokuGroup board, int rowLength, int squareHeight, int squareWidth, int blockRow, int blockCol, int index, bool setToFalse, int boardIndex, SudokuGroup fullBoard)
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
						VisitCell((SudokuCell) board.Components[index], false, board, boardIndex, index, fullBoard);
						stf = true;
						/*continue;*/
					}
					/*else if (!stf && !board.Components[index].IsCorrect && board.Components[index].Value != board.Components[checkIndex].Value && index != checkIndex)
					{
						VisitCell((SudokuCell) board.Components[index], true, board, boardIndex, index, fullBoard);
					}*/
				}
			}

			return stf;
		}
	}
}
