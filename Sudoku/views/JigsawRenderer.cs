using Sudoku.models.states;
using Sudoku.models.SudokuComponent;
using Sudoku.views;

namespace Sudoku.renderers
{
    public class JigsawRenderer : AbstractRenderer
    {
        public override object Clone()
        {
            return new JigsawRenderer();
        }

		public override void DrawBoard(SudokuGroup board, int squareLength, int squareHeight)
        {
			int rowLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));

			for (int i = 0; i < board.Components.Count; i++)
			{
				DrawCell(board.Components[i].Value, board.Components[i].Block, board.Components[i].IsCorrect, board.State, board.Components[i].IsFixed);

				if ((i + 1) % rowLength == 0 && (i + 1) < board.Components.Count)
				{
                    DrawLine();
                }
			}
		}

        public override void DrawNotes(SudokuGroup board, int squareLength, int squareHeight)
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

                    int? block = ((SudokuCell)board.Components[i / squareHeight * rowLength + j / squareLength]).Block;
                    DrawCell(int.Parse(notesMatrix[i, j]), block, true, board.State, false);
                }

                Console.WriteLine("|");
            }

            DrawHorizontalNoteSeperator(boardWidth, squareHeight, squareLength);
        }

        private void DrawCell(int value, int? Block, bool isCorrect, iBoardState state, bool isFixed)
		{
			ChangeColor(Block);

			if (!isCorrect && state is CorrectionState)
			{
				Console.ForegroundColor = ConsoleColor.Red;
			}

            if (isFixed && state is DefinitiveState)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }

            Console.Write(value == 0 ? " " : value.ToString());
			ChangeColor(null);
        }

        private void ChangeColor(int? Block)
		{
			Console.ForegroundColor = ConsoleColor.Black;

            Console.BackgroundColor = Block switch
            {
                0 => ConsoleColor.Yellow,
                1 => ConsoleColor.Cyan,
                2 => ConsoleColor.DarkGray,
                3 => ConsoleColor.DarkRed,
                4 => ConsoleColor.DarkCyan,
                5 => ConsoleColor.Blue,
                6 => ConsoleColor.Gray,
                7 => ConsoleColor.Magenta,
                8 => ConsoleColor.White,
                _ => ConsoleColor.Black 
            };

            if (Block == null || Block < 0 || Block > 8)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
	}
}
