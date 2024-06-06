using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
    public abstract class AbstractVisitor : iBoardVisitor
    {
        public abstract void VisitBoard(SudokuGroup board, int boardIndex, SudokuGroup fullBoard);

        public abstract void VisitJigsaw(SudokuGroup board);

        public void VisitCell(SudokuCell cell, SudokuGroup board, int boardIndex, int celIndex, SudokuGroup fullBoard)
        {
            if (boardIndex >= 0)
            {
                int totalCells = board.Components.Count;
                int rowLength = Convert.ToInt32(Math.Sqrt(totalCells));
                int squareLength = Convert.ToInt32(Math.Sqrt(rowLength));
                int index = -1;

                switch (boardIndex)
                {
                    case 0: // Upper left
                        if ((celIndex >= (rowLength * 6 + squareLength * 2) && celIndex < rowLength * 7) ||
                            (celIndex >= (rowLength * 7 + squareLength * 2) && celIndex < rowLength * 8) ||
                            (celIndex >= (rowLength * 8 + squareLength * 2) && celIndex < board.Components.Count))
                        {
                            index = celIndex - (rowLength * 6 + squareLength * 2);
                            fullBoard.Components[2].Components[index].IsCorrect = false;
                            break;
                        }
                        break;
                    case 1: // Upper right
                        if ((celIndex >= (rowLength * 6) && celIndex < (rowLength * 6 + squareLength)) ||
                            (celIndex >= (rowLength * 7) && celIndex < (rowLength * 7 + squareLength)) ||
                            (celIndex >= (rowLength * 8) && celIndex < (rowLength * 8 + squareLength)))
                        {
                            index = celIndex - rowLength * 6 + squareLength * 2;
                            fullBoard.Components[2].Components[index].IsCorrect = false;
                            break;
                        }
                        break;
                    case 2: // Middle
                        if ((celIndex >= 0 && celIndex < squareLength) ||
                            (celIndex >= rowLength && celIndex < (rowLength * 1 + squareLength)) ||
                            (celIndex >= (rowLength * 2) && celIndex < (rowLength * 2 + squareLength)))
                        {
                            index = celIndex + rowLength * 6 + squareLength * 2;
                            fullBoard.Components[0].Components[index].IsCorrect = false;
                            break;
                        }

                        if ((celIndex >= (squareLength * 2) && celIndex < rowLength * 1) ||
                            (celIndex >= (rowLength + squareLength * 2) && celIndex < rowLength * 2) ||
                            (celIndex >= (rowLength * 2 + squareLength * 2) && celIndex < (rowLength * 3)))
                        {
                            index = celIndex - squareLength * 2 + rowLength * 6;
                            fullBoard.Components[1].Components[index].IsCorrect = false;
                            break;
                        }

                        if ((celIndex >= (rowLength * 6) && celIndex < (rowLength * 6 + squareLength)) ||
                            (celIndex >= (rowLength * 7) && celIndex < (rowLength * 7 + squareLength)) ||
                            (celIndex >= (rowLength * 8) && celIndex < (rowLength * 8 + squareLength)))
                        {
                            index = celIndex - rowLength * 6 + squareLength * 2;
                            fullBoard.Components[3].Components[index].IsCorrect = false;
                            break;
                        }

                        if ((celIndex >= (rowLength * 6 + squareLength * 2) && celIndex < rowLength * 7) ||
                            (celIndex >= (rowLength * 7 + squareLength * 2) && celIndex < rowLength * 8) ||
                            (celIndex >= (rowLength * 8 + squareLength * 2) && celIndex < board.Components.Count))
                        {
                            index = celIndex - (rowLength * 6 + squareLength * 2);
                            fullBoard.Components[4].Components[index].IsCorrect = false;
                            break;
                        }
                        break;
                    case 3: // Lower left
                        if ((celIndex >= (squareLength * 2) && celIndex < rowLength * 1) ||
                            (celIndex >= (rowLength + squareLength * 2) && celIndex < rowLength * 2) ||
                            (celIndex >= (rowLength * 2 + squareLength * 2) && celIndex < (rowLength * 3)))
                        {
                            index = celIndex - squareLength * 2 + rowLength * 6;
                            fullBoard.Components[2].Components[index].IsCorrect = false;
                            break;
                        }
                        break;
                    case 4: // Lower right
                        if ((celIndex >= 0 && celIndex < squareLength) ||
                            (celIndex >= rowLength && celIndex < (rowLength * 1 + squareLength)) ||
                            (celIndex >= (rowLength * 2) && celIndex < (rowLength * 2 + squareLength)))
                        {
                            index = celIndex + rowLength * 6 + squareLength * 2;
                            fullBoard.Components[2].Components[index].IsCorrect = false;
                            break;
                        }
                        break;
                }
            }

            cell.IsCorrect = false;
        }
    }
}
