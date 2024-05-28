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
				DrawCell(board.Components[i].Value, board.Components[i].Block);

				if ((i + 1) % rowLength == 0 && (i + 1) < board.Components.Count)
				{
                    Console.WriteLine();
                }
			}
		}

		private void DrawCell(int value, int? Block)
		{
			ChangeColor(Block);
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
