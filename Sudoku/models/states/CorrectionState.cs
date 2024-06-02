using Sudoku.models.SudokuComponent;
using Sudoku.models.visitors;
using Sudoku.renderers;

namespace Sudoku.models.states
{
    public class CorrectionState : AbstractState
    {
		private RowVisitor rowVisitor = new RowVisitor();
		private ColumnVisitor columnVisitor = new ColumnVisitor();
		private SquareVisitor squareVisitor = new SquareVisitor();  

        public override void PrintState()

        {
            string message = "Board is now in Correction state. You can now see which sudoku cells are currently filled correctly and which are not." +
                "\n--> Press [+] to go to the definitive state or press [-] to go to the note state"; 
            string line = new string('-', GameController.START_LINE_LENGTH);
            Console.WriteLine($"{line}\n{message}\n{line}");
        }

        public override void DoAction(SudokuGroup board, GameController controller)
        {
			string message = "Red == incorrect";
            string line = new string('-', GameController.START_LINE_LENGTH);
            Console.WriteLine($"\n{line}\n{message}\n{line}");

            ReadInput(board, controller);
		}

        public void VisitVisitors(SudokuGroup board)
        {
			board.Accept(rowVisitor);
			board.Accept(columnVisitor);
			board.Accept(squareVisitor);
		}

		public void VisitVisitorsSamurai(SudokuGroup board)
		{
            foreach (SudokuGroup b in board.Components)
            {
				b.Accept(rowVisitor);
				b.Accept(columnVisitor);
				b.Accept(squareVisitor);
			}
		}

		public override void ReadInput(SudokuGroup board, GameController controller)
        {
            string input = Console.ReadLine() ?? "";
            CheckState(input, board, controller);
        }

        public override void DisplayBoard(iBoardRenderer renderer, SudokuGroup board, int length, int height)
        {
            renderer.DrawBoard(board, length, height);
        }
    }
}
