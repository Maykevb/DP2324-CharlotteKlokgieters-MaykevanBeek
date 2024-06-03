using Sudoku.models.SudokuComponent;

namespace Sudoku.models.visitors
{
    public interface iBoardVisitor
    {
        void Visit(SudokuCell cell);
        void Visit(SudokuGroup board);
    }
}
