using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
    public class RowVisitor : iBoardVisitor
    {
        public void VisitCell(SudokuCell cell, bool isCorrect)
        {
			cell.IsCorrect = isCorrect;
        }

        public void VisitBoard(SudokuGroup board)
        {
			int rowLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));
			bool setToFalse = false;

			for (int i = 0; i < rowLength; i++)
			{
				setToFalse = false;
				for (int j = 0; j < rowLength; j++)
				{
					int index = (i * rowLength) + j;
					if (index < board.Components.Count && !board.Components[index].IsFixed && board.Components[index].Value != 0)
					{
						setToFalse = CheckRow(board, rowLength, i, index, setToFalse);
					}
				}
			}
		}

		private bool CheckRow(SudokuGroup board, int rowLength, int i, int index, bool setToFalse)
		{
			bool stf = setToFalse;

			for (int k = 0; k < rowLength; k++)
			{
				int checkIndex = (i * rowLength) + k;
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
