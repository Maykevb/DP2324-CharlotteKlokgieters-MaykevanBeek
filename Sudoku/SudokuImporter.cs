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

        public SudokuBoard? readSudokuFromFile(SudokuType type)
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
                    return createBoard(sudoku, type);
                }
            }

            return null;
        }

        private SudokuBoard createBoard(string sudoku, SudokuType type)
        {
            SudokuBoard board = new SudokuBoard();

            switch (type)
            {
                case SudokuType.JIGSAW:
					board.Cells = createJigsawCells(sudoku);
                    break;
                case SudokuType.SAMURAI:
					board.Cells = createSamuraiCells(sudoku);
                    break;
                default:
					board.Cells = createCells(sudoku);
                    break;
			}
        
            return board;
        }

		private List<SudokuCell> createSamuraiCells(string sudoku)
        {
			string[] lines = sudoku.Split('\n');

			//TODO
			string line1 = lines[0];
			string line2 = lines[1];
			string line3 = lines[2];
			string line4 = lines[3];
			string line5 = lines[4];

			string result = "";

			for (int i = 0; i < 6; i++)
			{
				result += line1.Substring((i * 9), 9) + line2.Substring((i * 9), 9);
			}

			result += line1.Substring(54, 9) + line3.Substring(3, 3) + line2.Substring(54, 9);
			result += line1.Substring(63, 9) + line3.Substring(3 + 9, 3) + line2.Substring(63, 9);
			result += line1.Substring(72, 9) + line3.Substring(3 + 18, 3) + line2.Substring(72, 9);

			result += line3.Substring(27, 27);

			result += line4.Substring(0, 9) + line3.Substring(3 + 27 + 27, 3) + line5.Substring(0, 9);
			result += line4.Substring(9, 9) + line3.Substring(3 + 36 + 27, 3) + line5.Substring(9, 9);
			result += line4.Substring(18, 9) + line3.Substring(3 + 45 + 27, 3) + line5.Substring(18, 9);

			for (int i = 3; i < 9; i++)
			{
				result += line4.Substring((i * 9), 9) + line5.Substring((i * 9), 9);
			}

            List<SudokuCell> cells = createCells(result);
			return cells;
		}


		private List<SudokuCell> createCells(string sudoku)
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

        private List<SudokuCell> createJigsawCells(string sudoku)
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
