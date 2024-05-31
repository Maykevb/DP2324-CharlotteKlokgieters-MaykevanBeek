using Sudoku.models.SudokuComponent;
using System;
using System.Linq;

namespace Sudoku.models.visitors
{
	public class SquareVisitor : iBoardVisitor
	{
		public void VisitCell(SudokuCell cell)
		{
			
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

			int blocksPerRow = rowLength / squareHeight;
			int blocksPerCol = rowLength / squareWidth;

			for (int blockRow = 0; blockRow < blocksPerRow; blockRow++)
			{
				for (int blockCol = 0; blockCol < blocksPerCol; blockCol++)
				{
					ProcessBlock(board, rowLength, squareHeight, squareWidth, blockRow, blockCol);
				}
			}
		}

		private void ProcessBlock(SudokuGroup board, int rowLength, int squareHeight, int squareWidth, int blockRow, int blockCol)
		{
			int baseRowIndex = blockRow * squareHeight;
			int baseColIndex = blockCol * squareWidth;

			for (int i = 0; i < squareHeight; i++)
			{
				for (int j = 0; j < squareWidth; j++)
				{
					int rowIndex = baseRowIndex + i;
					int colIndex = baseColIndex + j;
					int cellIndex = rowIndex * rowLength + colIndex;

					if (cellIndex < board.Components.Count && board.Components[cellIndex].CorrectValue == 0)
					{
						FilterNotesInBlock(board, rowLength, squareHeight, squareWidth, blockRow, blockCol, cellIndex);
						AutoSolveCell(board, cellIndex, rowLength, squareHeight, squareWidth, blockRow, blockCol);
					}
				}
			}
		}

		private void FilterNotesInBlock(SudokuGroup board, int rowLength, int squareHeight, int squareWidth, int blockRow, int blockCol, int targetIndex)
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

					if (checkIndex < board.Components.Count && board.Components[checkIndex].CorrectValue != 0)
					{
						board.Components[targetIndex].AutoSolveNotes = board.Components[targetIndex].AutoSolveNotes
							.Where(val => val != board.Components[checkIndex].CorrectValue).ToArray();
					}
				}
			}
		}

		private void AutoSolveCell(SudokuGroup board, int targetIndex, int rowLength, int squareHeight, int squareWidth, int blockRow, int blockCol)
		{
			if (board.Components[targetIndex].AutoSolveNotes.Length == 1)
			{
				board.Components[targetIndex].CorrectValue = board.Components[targetIndex].AutoSolveNotes[0];
				board.Components[targetIndex].AutoSolveNotes = new int[0];
				return;
			}

			for (int value = 1; value <= rowLength; value++)
			{
				int occurrences = 0;
				int uniqueIndex = -1;
				int baseRowIndex = blockRow * squareHeight;
				int baseColIndex = blockCol * squareWidth;

				for (int m = 0; m < squareHeight; m++)
				{
					for (int n = 0; n < squareWidth; n++)
					{
						int checkRowIndex = baseRowIndex + m;
						int checkColIndex = baseColIndex + n;
						int checkIndex = checkRowIndex * rowLength + checkColIndex;

						if (checkIndex < board.Components.Count)
						{
							if (board.Components[checkIndex].CorrectValue == 0 &&
								board.Components[checkIndex].AutoSolveNotes.Contains(value))
							{
								occurrences++;
								uniqueIndex = checkIndex;
								continue;
							}

							if (board.Components[checkIndex].CorrectValue == value)
							{
								occurrences++;
							}
						}
					}
				}

				if (occurrences == 1 && uniqueIndex != -1)
				{
					board.Components[uniqueIndex].CorrectValue = value;
					board.Components[uniqueIndex].AutoSolveNotes = new int[0];
				}
			}
		}
	}
}
