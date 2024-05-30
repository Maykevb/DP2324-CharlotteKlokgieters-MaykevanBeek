using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
    public interface iBoardVisitor
    {
        void VisitCell(SudokuCell cell);

		void VisitBoard(SudokuGroup board);
    }
}
