namespace Sudoku
{
    using System;
    using System.IO;
    using System.Linq;

    public class SudokuImporter
    {
        public void readSudokuFromFile(SudokuType type)
        {
            string folderPath = $"resources/{type.ToString()}";
            Console.WriteLine(folderPath);
            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath);

                Console.WriteLine("test" + files.Length);
                if (files.Length > 0)
                {
                    Random random = new Random();
                    string sudokuFile = files[random.Next(files.Length)];
                    createBoard(sudokuFile);
                }
            }
        }

        private void createBoard(string sudokuFile)
        {
            Console.WriteLine(sudokuFile);
        }

        private void createCells()
        {

        }
    }
}
