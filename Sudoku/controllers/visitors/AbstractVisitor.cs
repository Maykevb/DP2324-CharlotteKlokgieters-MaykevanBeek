using Sudoku.models.SudokuComponent;
using System;

namespace Sudoku.models.visitors
{
	public abstract class AbstractVisitor : iBoardVisitor
	{
		public abstract void VisitBoard(SudokuGroup board, int boardIndex, SudokuGroup fullBoard);

		public abstract void VisitJigsaw(SudokuGroup board);

        public void VisitCell(SudokuCell cell, SudokuGroup board, int boardIndex, int cellIndex, SudokuGroup fullBoard)
        {
            Console.WriteLine(boardIndex);
            if (boardIndex >= 0)
            {
                int totalCells = board.Components.Count;
                int rowLength = Convert.ToInt32(Math.Sqrt(totalCells));
                int squareLength = Convert.ToInt32(Math.Sqrt(rowLength));

                int[][] boardRanges = SetBoardRanges(rowLength, squareLength, totalCells);

                int adjustedIndex = -1;
                int targetGroup = -1;

                for (int i = 0; i < boardRanges.Length; i++)
                {
                    if (cellIndex >= boardRanges[i][0] && cellIndex < boardRanges[i][1])
                    {
                        adjustedIndex = cellIndex - boardRanges[i][0];
                        targetGroup = boardRanges[i][2];
                        if (targetGroup == 0) adjustedIndex += rowLength * 6 + squareLength * 2;
                        else if (targetGroup == 1) adjustedIndex += rowLength * 6 - squareLength * 2;
                        else if (targetGroup == 3) adjustedIndex -= rowLength * 6 - squareLength * 2;
                        break;
                    }
                }

                if (targetGroup >= 0 && adjustedIndex >= 0 && adjustedIndex < fullBoard.Components[targetGroup].Components.Count)
                {
                    fullBoard.Components[targetGroup].Components[adjustedIndex].IsCorrect = false;
                    Console.WriteLine("target" + targetGroup + " " + fullBoard.Components[targetGroup].Components[adjustedIndex].IsCorrect + " " + adjustedIndex);
                }
            }

            cell.IsCorrect = false;

            Console.WriteLine(fullBoard.Components[2].Components[19].IsCorrect + " " + fullBoard.Components[2].Components[19].Value);
        }

        private int[][] SetBoardRanges(int rowLength, int squareLength, int totalCells)
		{
			return new int[][] {
				new int[] { rowLength * 6 + squareLength * 2, rowLength * 7, 2 },
				new int[] { rowLength * 7 + squareLength * 2, rowLength * 8, 2 },
				new int[] { rowLength * 8 + squareLength * 2, totalCells, 2 },
				new int[] { rowLength * 6, rowLength * 6 + squareLength, 2 },
				new int[] { rowLength * 7, rowLength * 7 + squareLength, 2 },
				new int[] { rowLength * 8, rowLength * 8 + squareLength, 2 },
				new int[] { 0, squareLength, 0 },
				new int[] { rowLength, rowLength + squareLength, 0 },
				new int[] { rowLength * 2, rowLength * 2 + squareLength, 0 },
				new int[] { squareLength * 2, rowLength, 1 },
				new int[] { rowLength + squareLength * 2, rowLength * 2, 1 },
				new int[] { rowLength * 2 + squareLength * 2, rowLength * 3, 1 },
				new int[] { rowLength * 6, rowLength * 6 + squareLength, 3 },
				new int[] { rowLength * 7, rowLength * 7 + squareLength, 3 },
				new int[] { rowLength * 8, rowLength * 8 + squareLength, 3 }
			};
		}
	}
}
