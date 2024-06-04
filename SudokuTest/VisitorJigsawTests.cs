using Sudoku.models.SudokuComponent;
using Sudoku.models.visitors;

namespace SudokuTest
{
    [TestClass]
    public class VisitorJigsawTests
    {
        GameController controller;
        iSudokuComponent board;

        [TestInitialize]
        public void Setup()
        {
            controller = new GameController();
            board = new SudokuGroup(controller, SudokuType.JIGSAW);

            String sudoku = "SumoCueV1=0J0=6J1=0J1=0J1=0J1=0J2=0J3=7J3=0J3=2J0=4J0=0J1=0J1=0J1=7J2=0J2=5J3=8J3=0J0=0J0=0J0=0J1=0J1=0J2=0J3=0J3=0J3=0J0=8J4=0J0=6J0=5J2=3J2=0J2=0J5=0J3=0J4=0J4=0J4=3J6=0J6=9J2=0J2=0J5=0J5=0J4=0J4=0J4=2J6=7J6=8J7=0J7=3J7=0J5=0J4=0J4=0J8=0J6=0J6=0J7=0J7=0J7=0J5=9J8=2J8=0J8=1J8=0J6=0J6=0J7=6J5=3J5=0J8=5J8=0J8=0J8=0J6=0J7=0J7=8J5=0J5";
            int rowLength = Convert.ToInt32(Math.Sqrt(sudoku.Length));

            List<iSudokuComponent> cells = new List<iSudokuComponent>();
            string cleanedSudoku = sudoku.Replace("SumoCueV1=", "");
            string[] cellData = cleanedSudoku.Split('=');

            foreach (var data in cellData)
            {
                if (data.Length < 3) continue;

                int value = -1;
                if (int.TryParse(data[0].ToString(), out int cellValue))
                {
                    value = cellValue;
                }

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

            board.Components = cells;
        }

        [TestMethod]
        public void TestRowVisitorJigsaw()
        {
            // Arrange
            RowVisitor visitor = new RowVisitor();
            board.Components[0].Value = 6;

            // Act 
            visitor.VisitBoard((SudokuGroup)board, -1, (SudokuGroup)board);

            // Assert
            Assert.AreEqual(board.Components[0].IsCorrect, false);
        }

        [TestMethod]
        public void TestColumnVisitorJigsaw()
        {
            // Arrange
            ColumnVisitor visitor = new ColumnVisitor();
            board.Components[0].Value = 2;

            // Act 
            visitor.VisitBoard((SudokuGroup)board, -1, (SudokuGroup)board);

            // Assert
            Assert.AreEqual(board.Components[0].IsCorrect, false);
        }

        [TestMethod]
        public void TestSquareVisitorJigsaw()
        {
            // Arrange
            SquareVisitor visitor = new SquareVisitor();
            board.Components[0].Value = 2;

            // Act 
            visitor.VisitJigsaw((SudokuGroup)board);

            // Assert
            Assert.AreEqual(board.Components[0].IsCorrect, false);
        }

    }
}
