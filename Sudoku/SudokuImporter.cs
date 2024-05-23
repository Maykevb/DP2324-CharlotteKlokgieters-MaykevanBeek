namespace Sudoku
{
    using Sudoku.models.BoardComponent;
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
	using System.Text;

    public class SudokuImporter
    {
		private static readonly int SAMURAI_SINGLE_ROW = 9;

        public SudokuBoard? ReadSudokuFromFile(SudokuType type)
        {
            string folderPath = $"../../../resources/{type.ToString()}";
            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath);

                if (files.Length > 0)
                {
                    Random random = new Random();
                    string sudokuFile = files[random.Next(files.Length)]; 
                    string sudoku = File.ReadAllText(sudokuFile);
                    return CreateBoard(sudoku, type);
                }
            }

            return null;
        }

        private SudokuBoard CreateBoard(string sudoku, SudokuType type)
        {
            SudokuBoard board = new SudokuBoard();

            switch (type)
            {
                case SudokuType.JIGSAW:
					board.Cells = CreateJigsawCells(sudoku);
                    break;
                case SudokuType.SAMURAI:
					board.Cells = CreateSamuraiCells(sudoku);
                    break;
                default:
					board.Cells = CreateCells(sudoku);
                    break;
			}
        
            return board;
        }

		private List<SudokuCell> CreateSamuraiCells(string sudoku)
        {
            const int row_length = 9;
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
			return cells;
		}


		private List<SudokuCell> CreateCells(string sudoku)
        {
            List<SudokuCell> cells = new List<SudokuCell>();

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

        private List<SudokuCell> CreateJigsawCells(string sudoku)
        {
            List<SudokuCell> cells = new List<SudokuCell>();
            string cleanedSudoku = sudoku.Replace("SumoCueV1=", "");

            string[] cellData = sudoku.Split('=');

            foreach (var data in cellData)
            {
                if (data.Length < 3) continue; 

                char value = data[0];
                char block = data[2];

                SudokuCell cell = new SudokuCell(value, true, block);

                cells.Add(cell);
            }

            return cells;
        }
	}
}
