using Sudoku.models.states;
using Sudoku.models.SudokuComponent;

namespace Sudoku.renderers
{
    public class StandardRenderer : iBoardRenderer
    {
        public object Clone()
        {
            return new StandardRenderer();
        }

		public void DrawBoard(SudokuGroup board, int squareLength, int squareHeight)
		{
			int rowLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));

			DrawHorizontalSeparator(rowLength, squareLength);

			for (int i = 0; i < board.Components.Count; i++)
			{
                if(i % rowLength == 0)
                {
                    DrawVerticalSeperator();
                }

				DrawCell(board.Components[i].Value, board.Components[i].IsCorrect, board.State);

				if ((i + 1) % squareLength == 0 && !((i + 1) % rowLength == 0) || (i + 1) % (squareHeight * squareLength) == 0)
				{
                    DrawVerticalSeperator();
                }

				if ((i + 1) % rowLength == 0 && (i + 1) % (squareHeight * rowLength) == 0)
				{
					DrawHorizontalSeparator(rowLength, squareLength);
					continue;
				}
				
				if ((i + 1) % rowLength == 0)
				{
					Console.WriteLine();
				}
			}
		}

        public void DrawNotes(SudokuGroup board, int squareLength, int squareHeight)
        {
            int rowLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));
            int boardWidth = rowLength * squareLength;
            int boardHeight = rowLength * squareHeight;

            DrawHorizontalNoteSeparator(boardWidth, squareHeight, squareLength);

            string[,] notesMatrix = CalculateNotes(board, squareLength, squareHeight);

            for (int i = 0; i < boardHeight; i++)
            {
                if (i % squareHeight == 0 && i != 0)
                {
                    DrawHorizontalNoteSeparator(boardWidth, squareHeight, squareLength);
                }

                DrawVerticalSeperator();

                for (int j = 0; j < boardWidth; j++)
                {
                    if (j % squareLength == 0 && j != 0)
                    {
                        DrawVerticalSeperator();
                    }

                    DrawCell(int.Parse(notesMatrix[i, j]), true, board.State);
                }

                Console.WriteLine("|");
            }

            DrawHorizontalNoteSeparator(boardWidth, squareHeight, squareLength);
        }

        private string[,] CalculateNotes(SudokuGroup board, int squareLength, int squareHeight)
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

        private void DrawVerticalSeperator()
		{
            Console.Write("|");
        }

		private void DrawCell(int value, bool isCorrect, iBoardState state)
		{           
			if (!isCorrect && state is CorrectionState)
			{
				Console.ForegroundColor = ConsoleColor.Red;
			}

			Console.Write(value == 0 ? " " : value.ToString());
			Console.ForegroundColor = ConsoleColor.White;
		}

        private void DrawHorizontalNoteSeparator(int boardWidth, int squareHeight, int squareLength)
        {
            int totalLength = boardWidth + 2 * (boardWidth / squareLength) - squareHeight * squareLength + 1;
            Console.WriteLine(new string('-', totalLength));
        }

        private void DrawHorizontalSeparator(int rowLength, int squareLength) 
		{
			Console.Write("\n" + new string('-', rowLength + (rowLength / squareLength) + 1) + "\n");
		}
	}
}
