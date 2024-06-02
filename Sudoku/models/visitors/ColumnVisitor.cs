using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
    public class ColumnVisitor : iBoardVisitor
    {
        public void VisitCell(SudokuCell cell, bool isCorrect)
        {
			cell.IsCorrect = isCorrect;
		}

        public void VisitBoard(SudokuGroup board)
        {
			int colLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));
			bool setToFalse = false;

			for (int i = 0; i < colLength; i++)
			{
				setToFalse = false;
				for (int j = 0; j < colLength; j++)
				{
					int index = i + (j * colLength);
					if (index < board.Components.Count && !board.Components[index].IsFixed && board.Components[index].Value != 0)
					{
						setToFalse = CheckCol(board, colLength, i, index, setToFalse);
					}
				}
			}
		}

		private bool CheckCol(SudokuGroup board, int colLength, int i, int index, bool setToFalse)
		{
			bool stf = setToFalse;

			for (int k = 0; k < colLength; k++)
			{
				int checkIndex = i + (k * colLength);
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

			return stf;
		}
	}
}
