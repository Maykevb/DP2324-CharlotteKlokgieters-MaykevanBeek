using Sudoku.models.SudokuComponent;
using Sudoku.models.visitors;

namespace SudokuTest
{
    [TestClass]
    public class VisitorSamuraiTests
    {
        GameController controller;
        iSudokuComponent board;

        [TestInitialize]
        public void Setup()
        {
            controller = new GameController();
            board = new SudokuGroup(controller, SudokuType.SAMURAI);

            String sudoku = "038009602076400000000000300500000406300008090007000010400600000000900000700010000\r\n002050310800200000000019000460000080200003600005006100000000900000000062000072000\r\n000400000000230000000700000000000060108000905070000000000002000000013000000005000\r\n000630000560000000008000000001300600004100008020000049000260000000005004096010500\r\n000050001000003000000004006040000100070500008803000005007000000000001960406700530";
            string[] lines = sudoku.Split("\r\n");

            List<iSudokuComponent> boards = new List<iSudokuComponent>();

            for (int i = 0; i < lines.Length; i++)
            {
                SudokuGroup board2 = new SudokuGroup(controller, SudokuType.SAMURAI);
                int rowLength = Convert.ToInt32(Math.Sqrt(lines[i].Length));

                List<iSudokuComponent> cells = new List<iSudokuComponent>();

                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (int.TryParse(lines[i][j].ToString(), out int cellValue) && cellValue != 0)
                    {
                        SudokuCell cell = new SudokuCell(cellValue, true, rowLength);
                        cells.Add(cell);
                    }
                    else if (int.TryParse(lines[i][j].ToString(), out int cellValue2) && cellValue2 == 0)
                    {
                        SudokuCell cell = new SudokuCell(cellValue2, false, rowLength);
                        cells.Add(cell);
                    }
                }

                board2.Components = cells;
                boards.Add(board2);
            }

            board.Components = boards;
        }

        [TestMethod]
        public void TestRowVisitorSamurai()
        {
            // Arrange
            RowVisitor visitor = new RowVisitor();
            board.Components[0].Components[0].Value = 3;

            // Act 
            visitor.VisitBoard((SudokuGroup)board.Components[0], 0, (SudokuGroup)board);

            // Assert
            Assert.AreEqual(board.Components[0].Components[0].IsCorrect, false);
        }

        [TestMethod]
        public void TestColumnVisitorSamurai()
        {
            // Arrange
            ColumnVisitor visitor = new ColumnVisitor();
            board.Components[0].Components[0].Value = 5;
            SudokuGroup samuraiBoard = (SudokuGroup)board;
            SudokuGroup samuraigroup = (SudokuGroup)board.Components[0];

            // Act 
            visitor.VisitBoard(samuraigroup, 0, samuraiBoard);

            // Assert
            Assert.AreEqual(board.Components[0].Components[0].IsCorrect, false);
        }

        [TestMethod]
        public void TestSquareVisitorSamuraiAsync()
        {
            // Arrange
            SquareVisitor visitor = new SquareVisitor();
            board.Components[0].Components[0].Value = 6;

            // Act 
            visitor.VisitBoard((SudokuGroup)board.Components[0], 0, (SudokuGroup)board);

            // Assert
            Assert.AreEqual(board.Components[0].Components[0].IsCorrect, false);
        }

        [TestMethod]
        public void TestSquareVisitorSamuraiOverlapAsync()
        {
            // Arrange
            SquareVisitor visitor = new SquareVisitor();
            board.Components[0].Components[80].Value = 6;
            board.Components[0].Components[79].Value = 6;

            // Act 
            visitor.VisitBoard((SudokuGroup)board.Components[0], 0, (SudokuGroup)board);

            // Assert
            Assert.AreEqual(board.Components[2].Components[20].IsCorrect, false);
        }

        [TestMethod]
        public void TestRowVisitorSamuraiOverlapAsync()
        {
            // Arrange
            RowVisitor visitor = new RowVisitor();
            board.Components[0].Components[80].Value = 1;

            // Act 
            visitor.VisitBoard((SudokuGroup)board.Components[0], 0, (SudokuGroup)board);

            // Assert
            Assert.AreEqual(board.Components[2].Components[20].IsCorrect, false);
        }

        [TestMethod]
        public void TestColumnVisitorSamuraiOverlapAsync()
        {
            // Arrange
            ColumnVisitor visitor = new ColumnVisitor();
            board.Components[0].Components[80].Value = 2;

            // Act 
            visitor.VisitBoard((SudokuGroup)board.Components[0], 0, (SudokuGroup)board);

            // Assert
            Assert.AreEqual(board.Components[2].Components[20].IsCorrect, false);
        }
    }
}
