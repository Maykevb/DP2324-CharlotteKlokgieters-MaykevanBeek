namespace Sudoku
{
    using Sudoku.models.BoardComponent;
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class SudokuImporter
    {
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

            if (type != SudokuType.JIGSAW)
            {
                board.Cells = createCells(sudoku);
            } 
            else
            {
                board.Cells = createJigsawCells(sudoku);
            }
        
            return board;
        }

        private List<SudokuCell> createCells(string sudoku)
        {
            List<SudokuCell> cells = new List<SudokuCell>();

            for (int i = 0; i < sudoku.Length; i++)
            {
				// Convert the character to an integer
				int cellValue = sudoku[i] - '0';

				SudokuCell cell = new SudokuCell(cellValue, true);
                cells.Add(cell);
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
