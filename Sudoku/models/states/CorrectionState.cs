using Sudoku.models.SudokuComponent;
using Sudoku.models.visitors;

namespace Sudoku.models.states
{
    public class CorrectionState : iBoardState
    {
		private RowVisitor rowVisitor = new RowVisitor();
		private ColumnVisitor columnVisitor = new ColumnVisitor();
		private SquareVisitor squareVisitor = new SquareVisitor();  

		public void PrintState()
        {
            string message = "Board is now in Correction state. You can now see which sudoku cells are currently filled correctly and which are not." +
                "\n--> Press [+] to go to the definitive state or press [-] to go to the note state"; 
            string line = new string('-', GameController.START_LINE_LENGTH);
            Console.WriteLine($"{line}\n{message}\n{line}");
        }

        public void DoAction(SudokuGroup board)
        {
			VisitVisitors(board); //TODO placement ??

			string message = "Red == incorrect";
            string line = new string('-', GameController.START_LINE_LENGTH);
            Console.WriteLine($"\n{line}\n{message}\n{line}");
        }

        private void VisitVisitors(SudokuGroup board)
        {
			board.Accept(rowVisitor);
			board.Accept(columnVisitor);
			board.Accept(squareVisitor);
		}
    }
}
