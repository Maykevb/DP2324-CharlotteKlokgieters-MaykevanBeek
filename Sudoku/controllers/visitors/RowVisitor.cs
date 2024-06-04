using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
    public class RowVisitor : AbstractVisitor
    {
        public override void VisitBoard(SudokuGroup board, int boardIndex, SudokuGroup fullBoard)
        {
			int rowLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));

			for (int i = 0; i < rowLength; i++)
			{
				for (int j = 0; j < rowLength; j++)
				{
					int index = (i * rowLength) + j;
					if (index < board.Components.Count && !board.Components[index].IsFixed && board.Components[index].Value != 0)
					{
						CheckRow(board, rowLength, i, index, boardIndex, fullBoard);
					}
				}
			}
		}

		public override void VisitJigsaw(SudokuGroup board) 
		{

		}

		private void CheckRow(SudokuGroup board, int rowLength, int i, int index, int boardIndex, SudokuGroup fullBoard)
		{
			for (int k = 0; k < rowLength; k++)
			{
				int checkIndex = (i * rowLength) + k;
				if (checkIndex < board.Components.Count && board.Components[index].Value == board.Components[checkIndex].Value && index != checkIndex)
				{
					board.Components[index].Accept(this, board, boardIndex, index, fullBoard);
				}	
			}
		}
	}
}
