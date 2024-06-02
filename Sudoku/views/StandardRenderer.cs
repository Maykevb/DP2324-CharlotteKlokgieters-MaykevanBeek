using Sudoku.models.states;
using Sudoku.models.SudokuComponent;
using Sudoku.views;

namespace Sudoku.renderers
{
    public class StandardRenderer : AbstractRenderer
    {
        public override object Clone()
        {
            return new StandardRenderer();
        }

		public override void DrawBoard(SudokuGroup board, int squareLength, int squareHeight)
		{
			int rowLength = Convert.ToInt32(Math.Sqrt(board.Components.Count));

			DrawHorizontalSeparator(rowLength, squareLength);

			for (int i = 0; i < board.Components.Count; i++)
			{
                if(i % rowLength == 0)
                {
                    DrawVerticalSeperator();
                }

				DrawCell(board.Components[i].Value);

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

                    DrawCell(int.Parse(notesMatrix[i, j]));
                }

                Console.WriteLine("|");
            }

            DrawHorizontalNoteSeperator(boardWidth, squareHeight, squareLength);
        }

        private void DrawHorizontalSeparator(int rowLength, int squareLength) 
		{
			Console.Write("\n" + new string('-', rowLength + (rowLength / squareLength) + 1) + "\n");
		}
	}
}
