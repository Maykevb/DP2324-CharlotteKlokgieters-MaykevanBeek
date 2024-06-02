namespace Sudoku
{
    using Sudoku.models.SudokuComponent;
    using System;
    using System.IO;

    public class SudokuImporter
    {
        public SudokuGroup? ReadSudokuFromFile(SudokuType type, GameController gameController)

        {
            string folderPath = $"../../../resources/{type.ToString()}";
            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath);

                if (files.Length > 0)
                {
                    Random random = new Random();
                    string sudokuFile = files[1]; //random.Next(files.Length)

					string sudoku = File.ReadAllText(sudokuFile);
                    return CreateBoard(gameController, sudoku, type);
                }
            }

            return null;
        }


        private SudokuGroup CreateBoard(GameController gameController, string sudoku, SudokuType type)
        {
			SudokuGroup board = new SudokuGroup(gameController, type);

            switch (type)
            {
                case SudokuType.JIGSAW:
					board.Components = CreateJigsawCells(sudoku);
					break;
                case SudokuType.SAMURAI:
					board.Components = CreateSamuraiBoards(sudoku, gameController, type);
					break;
                default:
					board.Components = CreateCells(sudoku);
                    break;
			}
        
            return board;
        }

		private List<iSudokuComponent> CreateSamuraiBoards(string sudoku, GameController gameController, SudokuType type)
        {
			string[] lines = sudoku.Split('\n');

			List<iSudokuComponent> boards = new List<iSudokuComponent>();

			for (int i = 0; i < lines.Length; i++)
            {
				SudokuGroup board = new SudokuGroup(gameController, type);
				board.Components = CreateCells(lines[i]);
				boards.Add(board);
			}

			return boards;
		}


		private List<iSudokuComponent> CreateCells(string sudoku)
        {
			int rowLength = Convert.ToInt32(Math.Sqrt(sudoku.Length));

			List<iSudokuComponent> cells = new List<iSudokuComponent>();

            for (int i = 0; i < sudoku.Length; i++)
            {
				if (int.TryParse(sudoku[i].ToString(), out int cellValue) && cellValue != 0)
				{
					SudokuCell cell = new SudokuCell(cellValue, true, rowLength);
					cells.Add(cell);
				} 
                else if (int.TryParse(sudoku[i].ToString(), out int cellValue2) && cellValue2 == 0)
                {
                    SudokuCell cell = new SudokuCell(cellValue2, false, rowLength);
                    cells.Add(cell);
                }
			}

			return cells;
        }

        private List<iSudokuComponent> CreateJigsawCells(string sudoku)
        {
			int rowLength = Convert.ToInt32(Math.Sqrt(sudoku.Length));

			List<iSudokuComponent> cells = new List<iSudokuComponent>();
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

                if (int.TryParse(data[0].ToString(), out int cellValue3) && cellValue3 != 0)
                {
                    SudokuCell cell = new SudokuCell(value, true, rowLength, block);
                    cells.Add(cell);
                }
                else if (int.TryParse(data[0].ToString(), out int cellValue4) && cellValue4 == 0)
                {
                    SudokuCell cell = new SudokuCell(value, false, rowLength, block);
                    cells.Add(cell);
                }
            }

            return cells;
        }
	}
}
