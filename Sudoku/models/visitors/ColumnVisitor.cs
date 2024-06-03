using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
    public class ColumnVisitor : AbstractVisitor
	{
        public override void VisitBoard(SudokuGroup board, int boardIndex, SudokuGroup fullBoard)
        {
			int colLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));

			for (int i = 0; i < colLength; i++)
			{
				for (int j = 0; j < colLength; j++)
				{
					int index = i + (j * colLength);
					if (index < board.Components.Count && !board.Components[index].IsFixed && board.Components[index].Value != 0)
					{
						CheckCol(board, colLength, i, index, boardIndex, fullBoard);
					}
				}
			}
		}

		public override void VisitJigsaw(SudokuGroup board) 
		{

		}

		private void CheckCol(SudokuGroup board, int colLength, int i, int index, int boardIndex, SudokuGroup fullBoard)
		{
			for (int k = 0; k < colLength; k++)
			{
				int checkIndex = i + (k * colLength);
				if (checkIndex < board.Components.Count && board.Components[index].Value == board.Components[checkIndex].Value && index != checkIndex)
				{
					board.Components[index].Accept(this, board, boardIndex, index, fullBoard);
				}
			}
		}
	}
}
