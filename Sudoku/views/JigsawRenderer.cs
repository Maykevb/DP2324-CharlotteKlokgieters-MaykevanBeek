using Sudoku.models.SudokuComponent;

namespace Sudoku.renderers
{
    public class JigsawRenderer : iBoardRenderer
    {
        public object Clone()
        {
            return new JigsawRenderer();
        }

		public void DrawBoard(SudokuGroup board, int squareLength, int squareHeight)
        {
			int rowLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));

			for (int i = 0; i < board.Components.Count; i++)
			{
				DrawCell(board.Components[i].Value, board.Components[i].Block, board.Components[i].IsCorrect);

				if ((i + 1) % rowLength == 0 && (i + 1) < board.Components.Count)
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

                    int? block = ((SudokuCell) board.Components[i / squareHeight * rowLength + j / squareLength]).Block;
                    bool isCorrect = ((SudokuCell)board.Components[i / squareHeight * rowLength + j / squareLength]).IsCorrect;
					DrawCell(int.Parse(notesMatrix[i, j]), block, isCorrect);
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

        private void DrawCell(int value, int? Block, bool isCorrect)
		{
			ChangeColor(Block);

			if (!isCorrect)
			{
				Console.ForegroundColor = ConsoleColor.Red;
			}

			Console.Write(value == 0 ? " " : value.ToString());
			ChangeColor(null);
        }

        private void DrawVerticalSeperator()
        {
            Console.Write("|");
        }

        private void DrawHorizontalNoteSeparator(int boardWidth, int squareHeight, int squareLength)
        {
            int totalLength = boardWidth + 2 * (boardWidth / squareLength) - squareHeight * squareLength + 1;
            Console.WriteLine(new string('-', totalLength));
        }

        private void ChangeColor(int? Block)
		{
			Console.ForegroundColor = ConsoleColor.Black;

            Console.BackgroundColor = Block switch
            {
                0 => ConsoleColor.Yellow,
                1 => ConsoleColor.Cyan,
                2 => ConsoleColor.Magenta,
                3 => ConsoleColor.Green,
                4 => ConsoleColor.Red,
                5 => ConsoleColor.Blue,
                6 => ConsoleColor.Gray,
                7 => ConsoleColor.DarkYellow,
                8 => ConsoleColor.DarkCyan,
                _ => ConsoleColor.Black 
            };

            if (Block == null || Block < 0 || Block > 8)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
	}
}
