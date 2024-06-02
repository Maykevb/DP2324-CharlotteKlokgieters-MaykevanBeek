using Sudoku.models.SudokuComponent;
using Sudoku.renderers;

namespace Sudoku.views
{
    public abstract class AbstractRenderer : iBoardRenderer
    {
        public abstract void DrawBoard(SudokuGroup board, int squareLength, int squareHeight);

        public abstract void DrawNotes(SudokuGroup board, int squareLength, int squareHeight);

        public abstract object Clone();

        protected string[,] CalculateNotes(SudokuGroup board, int squareLength, int squareHeight)
        {
            int rowLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));
            string[,] notesMatrix = new string[rowLength * squareHeight, rowLength * squareLength];

            for (int row = 0; row < rowLength; row++)
            {
                for (int col = 0; col < rowLength; col++)
                {
                    for (int subRow = 0; subRow < squareHeight; subRow++)
                    {
                        for (int subCol = 0; subCol < squareLength; subCol++)
                        {
                            int matrixRow = row * squareHeight + subRow;
                            int matrixCol = col * squareLength + subCol;
                            int cellNoteIndex = subRow * squareLength + subCol;

                            notesMatrix[matrixRow, matrixCol] = ((SudokuCell)board.Components[row * rowLength + col]).Notes[cellNoteIndex].ToString();
                        }
                    }
                }
            }

            return notesMatrix;
        }

        protected void DrawHorizontalNoteSeperator(int boardWidth, int squareHeight, int squareLength)
        {
            int totalLength = boardWidth + 2 * (boardWidth / squareLength) - squareHeight * squareLength + 1;
            Console.WriteLine(new string('-', totalLength));
        }

        protected void DrawVerticalSeperator()
        {
            Console.Write("|");
        }

        protected void DrawCell(int value)
        {
            Console.Write(value == 0 ? " " : value.ToString());
        }

        protected void DrawLine()
        {
            Console.WriteLine();
        }
    }
}
