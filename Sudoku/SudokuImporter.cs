namespace Sudoku
{
    using Sudoku.models.BoardComponent;
    using System;
    using System.IO;

    public class SudokuImporter
    {
		private static readonly int SAMURAI_SINGLE_ROW = 9;

        public SudokuGroup? ReadSudokuFromFile(SudokuType type)
        {
            string folderPath = $"../../../resources/{type.ToString()}";
            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath);

                if (files.Length > 0)
                {
                    Random random = new Random();
                    string sudokuFile = files[0]; // TODO random.Next(files.Length)
					string sudoku = File.ReadAllText(sudokuFile);
                    return CreateBoard(sudoku, type);
                }
            }

            return null;
        }

        private SudokuGroup CreateBoard(string sudoku, SudokuType type)
        {
			SudokuGroup board = new SudokuGroup();

            switch (type)
            {
                case SudokuType.JIGSAW:
					board.Components = CreateJigsawCells(sudoku);
					break;
                case SudokuType.SAMURAI:
					board.Components = CreateSamuraiBoards(sudoku);
					/*board.Cells = CreateSamuraiBoards(sudoku);*/
					break;
                default:
					board.Components = CreateCells(sudoku);
                    break;
			}
        
            return board;
        }

		private List<iBoardComponent> CreateSamuraiBoards(string sudoku)
        {
			/*const int row_length = 9;
            const int amount_shared_rows = 6;
            const int square_length = 3;
            const int cell_amount_mid = row_length * square_length;

			string[] lines = sudoku.Split('\n');
			string result = "";

			for (int i = 0; i < amount_shared_rows; i++)
			{
				result += lines[0].Substring((i * row_length), row_length) + lines[1].Substring((i * row_length), row_length);
			}

            for (int i = 0; i < square_length; i++)
            {
				result += lines[0].Substring((i + amount_shared_rows) * row_length, row_length) + lines[2].Substring(square_length + (i * row_length), square_length) + lines[1].Substring((i + amount_shared_rows) * row_length, row_length);
			}

			result += lines[2].Substring(cell_amount_mid, cell_amount_mid);

			for (int i = 0; i < square_length; i++)
			{
				result += lines[3].Substring((i * row_length), row_length) + lines[2].Substring(square_length + (cell_amount_mid + (i * row_length)) + cell_amount_mid, square_length) + lines[4].Substring((i * row_length), row_length);
			}

			for (int i = 3; i < row_length; i++)
			{
				result += lines[3].Substring((i * row_length), row_length) + lines[4].Substring((i * row_length), row_length);
			}

            List<SudokuCell> cells = CreateCells(result);
			return cells;*/

			string[] lines = sudoku.Split('\n');

			List<iBoardComponent> boards = new List<iBoardComponent>();

			for (int i = 0; i < lines.Length; i++)
            {
				SudokuGroup board = new SudokuGroup();
				board.Components = CreateCells(lines[i]);
				boards.Add(board);
			}

			return boards;
		}


		private List<iBoardComponent> CreateCells(string sudoku)
        {
			List<iBoardComponent> cells = new List<iBoardComponent>();

            for (int i = 0; i < sudoku.Length; i++)
            {
				// Convert the character to an integer
				if (int.TryParse(sudoku[i].ToString(), out int cellValue))
				{
					SudokuCell cell = new SudokuCell(cellValue, true);
					cells.Add(cell);
				}
			}

			return cells;
        }

        private List<iBoardComponent> CreateJigsawCells(string sudoku)
        {
			List<iBoardComponent> cells = new List<iBoardComponent>();
            string cleanedSudoku = sudoku.Replace("SumoCueV1=", "");
            string[] cellData = cleanedSudoku.Split('=');

            foreach (var data in cellData)
            {
                if (data.Length < 3) continue;

				// Convert the character to an integer
				int value = -1;
				if (int.TryParse(data[0].ToString(), out int cellValue))
                {
					value = cellValue;
				}

				// Convert the character to an integer
				int block = -1;
				if (int.TryParse(data[2].ToString(), out int cellValue2))
				{
					block = cellValue2;
				}

                SudokuCell cell = new SudokuCell(value, true, block);

                cells.Add(cell);
            }

            return cells;
        }
	}
}
