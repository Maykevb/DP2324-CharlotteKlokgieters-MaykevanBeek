using Sudoku.models.SudokuComponent;
using System;

namespace Sudoku.models.visitors
{
    public class SquareVisitor : iBoardVisitor
    {
        public void VisitCell(SudokuCell cell)
        {

        }

        public void VisitBoard(SudokuGroup board)
        {
			int rowLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));
			int squareLength = Convert.ToInt32(Math.Sqrt(rowLength));
			int squareHeight = squareLength; 

			// Adjust for 6x6 Sudoku if necessary
			if (board.Type == SudokuType.SIX_BY_SIX)
			{
				squareLength = 3;
				squareHeight = 2;
			}

			for (int blockRow = 0; blockRow < rowLength / squareHeight; blockRow++)
			{
				for (int blockCol = 0; blockCol < rowLength / squareLength; blockCol++)
				{
					for (int i = 0; i < squareHeight; i++)
					{
						for (int j = 0; j < squareLength; j++)
						{
							int rowIndex = blockRow * squareHeight + i;
							int colIndex = blockCol * squareLength + j;
							int index = rowIndex * rowLength + colIndex;

							if (index < board.Components.Count && board.Components[index].CorrectValue == 0)
							{

								// Check all cells in the current square
								for (int m = 0; m < squareHeight; m++)
								{
									for (int n = 0; n < squareLength; n++)
									{
										int checkRowIndex = blockRow * squareHeight + m;
										int checkColIndex = blockCol * squareLength + n;
										int checkIndex = checkRowIndex * rowLength + checkColIndex;

										if (checkIndex < board.Components.Count && board.Components[checkIndex].CorrectValue != 0)
										{
											board.Components[index].AutoSolveNotes = board.Components[index].AutoSolveNotes
												.Where(val => val != board.Components[checkIndex].CorrectValue).ToArray();
										}
									}
								}

								if (board.Components[index].AutoSolveNotes.Length == 1)
								{
									board.Components[index].CorrectValue = board.Components[index].AutoSolveNotes[0];
									board.Components[index].AutoSolveNotes = new int[0];
									continue;
								}

								// Check for unique possible values in the square
								for (int value = 1; value <= rowLength; value++) // Possible values range from 1 to rowLength
								{
									int occurrences = 0;
									int uniqueIndex = -1;

									for (int m = 0; m < squareHeight; m++)
									{
										for (int n = 0; n < squareLength; n++)
										{
											int checkRowIndex = blockRow * squareHeight + m;
											int checkColIndex = blockCol * squareLength + n;
											int checkIndex = checkRowIndex * rowLength + checkColIndex;

											if (checkIndex < board.Components.Count)
											{
												if (board.Components[checkIndex].CorrectValue == 0 &&
													board.Components[checkIndex].AutoSolveNotes.Contains(value))
												{
													occurrences++;
													uniqueIndex = checkIndex;
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
				}
			}
		}
	}
}
