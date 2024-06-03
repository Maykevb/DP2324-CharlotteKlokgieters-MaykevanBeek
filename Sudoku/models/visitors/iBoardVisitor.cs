using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
    public interface iBoardVisitor
    {
        void VisitCell(SudokuCell cell, SudokuGroup board, int boardIndex, int celIndex, SudokuGroup fullBoard);

		void VisitBoard(SudokuGroup board, int boardIndex, SudokuGroup fullBoard);

		void VisitJigsaw(SudokuGroup board);
	}
}
