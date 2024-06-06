using Sudoku.models.SudokuComponent;
using Sudoku.models.visitors;

namespace SudokuTest
{
    [TestClass]
    public class VisitorStandardTests
    {
        GameController controller;
        iSudokuComponent board;

        [TestInitialize]
        public void Setup()
        {
            controller = new GameController();
            board = new SudokuGroup(controller, SudokuType.FOUR_BY_FOUR);

            String sudoku = "0340400210030210";
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

            board.Components = cells;
        }

        [TestMethod]
        public void TestRowVisitorStandard()
        {
            // Arrange
            RowVisitor visitor = new RowVisitor();
            board.Components[0].Value = 3;

            // Act 
            visitor.VisitBoard((SudokuGroup)board, -1, (SudokuGroup)board);

            // Assert
            Assert.AreEqual(board.Components[0].IsCorrect, false);
        }

        [TestMethod]
        public void TestColumnVisitorStandard()
        {
            // Arrange
            ColumnVisitor visitor = new ColumnVisitor();
            board.Components[3].Value = 3;

            // Act 
            visitor.VisitBoard((SudokuGroup)board, -1, (SudokuGroup)board);

            // Assert
            Assert.AreEqual(board.Components[3].IsCorrect, false);
        }

        [TestMethod]
        public void TestSquareVisitorStandard()
        {
            // Arrange
            SquareVisitor visitor = new SquareVisitor();
            board.Components[10].Value = 1;

            // Act 
            visitor.VisitBoard((SudokuGroup)board, -1, (SudokuGroup)board);

            // Assert
            Assert.AreEqual(board.Components[10].IsCorrect, false);
        }
    }
}