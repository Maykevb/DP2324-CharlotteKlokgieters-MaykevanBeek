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
			int rowLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));

			for (int i = 0; i < rowLength; i++)
			{
				for (int j = 0; j < rowLength; j++)
				{
					int index = i + (j * rowLength);
					if (index < board.Components.Count && board.Components[index].CorrectValue == 0)
					{
						// Check all cells in the current column
						for (int k = 0; k < rowLength; k++)
						{
							int checkIndex = i + (k * rowLength);
							if (checkIndex < board.Components.Count && board.Components[checkIndex].CorrectValue != 0)
							{
								board.Components[index].AutoSolveNotes = board.Components[index].AutoSolveNotes
									.Where(val => val != board.Components[checkIndex].CorrectValue).ToArray();
							}
						}

						if (board.Components[index].AutoSolveNotes.Length == 1)
						{
							board.Components[index].CorrectValue = board.Components[index].AutoSolveNotes[0];
							board.Components[index].AutoSolveNotes = new int[rowLength];
							continue;
						}

						// Check for unique possible values in the column
						for (int value = 1; value <= rowLength; value++)
						{
							int occurrences = 0;
							int uniqueIndex = -1;

							for (int k = 0; k < rowLength; k++)
							{
								int checkIndex = i + (k * rowLength);
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
		}
	}
}
