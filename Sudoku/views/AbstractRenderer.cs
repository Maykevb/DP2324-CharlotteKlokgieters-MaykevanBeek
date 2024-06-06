using Sudoku.models.states;
using Sudoku.models.SudokuComponent;
using Sudoku.renderers;

namespace Sudoku.views
{
    public abstract class AbstractRenderer : iBoardRenderer
    {
        public abstract object Clone();

        public abstract void DrawBoard(SudokuGroup board, int squareLength, int squareHeight);

        public virtual void DrawNotes(SudokuGroup board, int squareLength, int squareHeight)
        {
            int rowLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));
            int boardWidth = rowLength * squareLength;
            int boardHeight = rowLength * squareHeight;

            DrawHorizontalNoteSeperator(boardWidth, squareHeight, squareLength);

            string[,] notesMatrix = CalculateNotes(board, squareLength, squareHeight);

            for (int i = 0; i < boardHeight; i++)
            {
                if (i % squareHeight == 0 && i != 0)
                {
                    DrawHorizontalNoteSeperator(boardWidth, squareHeight, squareLength);
                }

                DrawVerticalSeperator();

                for (int j = 0; j < boardWidth; j++)
                {
                    if (j % squareLength == 0 && j != 0)
                    {
                        DrawVerticalSeperator();
                    }

                    DrawCell(int.Parse(notesMatrix[i, j]), true, board.State, false);
                }

                Console.WriteLine("|");
            }

            DrawHorizontalNoteSeperator(boardWidth, squareHeight, squareLength);
        }

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

		protected void DrawCell(int value, bool isCorrect, iBoardState state, bool isFixed)
		{
			if (!isCorrect && state is CorrectionState)
			{
				Console.ForegroundColor = ConsoleColor.Red;
			} else if (isFixed && state is not NoteState)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }

			Console.Write(value == 0 ? " " : value.ToString());
			Console.ForegroundColor = ConsoleColor.White;
		}

		protected void DrawLine()
        {
            Console.WriteLine();
        }
    }
}

