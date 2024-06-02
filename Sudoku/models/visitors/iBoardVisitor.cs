using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
    public interface iBoardVisitor
    {
        void VisitCell(SudokuCell cell, bool isCorrect);

		void VisitBoard(SudokuGroup board);
    }
}
