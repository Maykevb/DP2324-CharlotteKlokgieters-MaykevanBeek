using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
    public class ColumnVisitor : iBoardVisitor
    {
        public void VisitCell(SudokuCell cell)
        {

        }

        public void VisitBoard(SudokuGroup board)
        {
			int colLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));

			for (int i = 0; i < colLength; i++)
			{
				for (int j = 0; j < colLength; j++)
				{
					int index = i + (j * colLength);
					if (index < board.Components.Count && board.Components[index].CorrectValue == 0)
					{
						FilterNotesInBlock(board, colLength, i, index);
						AutoSolveCell(board, index, colLength, i);
					}
				}
			}
		}

		private void FilterNotesInBlock(SudokuGroup board, int colLength, int i, int index)
		{
			for (int k = 0; k < colLength; k++)
			{
				int checkIndex = i + (k * colLength);
				if (checkIndex < board.Components.Count && board.Components[checkIndex].CorrectValue != 0)
				{
					board.Components[index].AutoSolveNotes = board.Components[index].AutoSolveNotes
						.Where(val => val != board.Components[checkIndex].CorrectValue).ToArray();
				}
			}
		}

		private void AutoSolveCell(SudokuGroup board, int index, int colLength, int i)
		{
			if (board.Components[index].AutoSolveNotes.Length == 1)
			{
				board.Components[index].CorrectValue = board.Components[index].AutoSolveNotes[0];
				board.Components[index].AutoSolveNotes = new int[colLength];
				return;
			}

			// Check for unique possible values in the column
			for (int value = 1; value <= colLength; value++)
			{
				int occurrences = 0;
				int uniqueIndex = -1;

				for (int k = 0; k < colLength; k++)
				{
					int checkIndex = i + (k * colLength);
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

				if (occurrences == 1 && uniqueIndex != -1)
				{
					board.Components[uniqueIndex].CorrectValue = value;
					board.Components[uniqueIndex].AutoSolveNotes = new int[0];
				}
			}
		}
	}
}
