using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
    public class RowVisitor : iBoardVisitor
    {
        public void VisitCell(SudokuCell cell, bool isCorrect, SudokuGroup board, int boardIndex, int celIndex, SudokuGroup fullBoard)
        {
			Console.WriteLine(cell.Value + isCorrect.ToString()); //TODO get rid off
			Console.WriteLine(boardIndex);
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
						setToFalse = CheckRow(board, rowLength, i, index, setToFalse, boardIndex, fullBoard);
					}
				}
			}
		}

		private bool CheckRow(SudokuGroup board, int rowLength, int i, int index, bool setToFalse, int boardIndex, SudokuGroup fullBoard)
		{
			bool stf = setToFalse;

			for (int k = 0; k < rowLength; k++)
			{
				int checkIndex = (i * rowLength) + k;
				if (checkIndex < board.Components.Count && board.Components[index].Value == board.Components[checkIndex].Value && index != checkIndex)
				{
					board.Components[index].Accept(this, false, board, boardIndex, index, fullBoard);
					/*VisitCell();*/
					stf = true;
					/*continue;*/
				}
				/*else if (!stf && !board.Components[index].IsCorrect && board.Components[index].Value != board.Components[checkIndex].Value && index != checkIndex)
				{
					board.Components[index].Accept(this, true, board, boardIndex, index, fullBoard);
					*//*VisitCell();*//*
				}*/				
			}

			return stf;
		}
	}
}
