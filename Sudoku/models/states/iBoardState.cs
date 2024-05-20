using Sudoku.models.BoardComponent;

namespace Sudoku.models.states
{
    public interface iBoardState
    {
        public void Handle(SudokuBoard board);
        iBoardState goNext();
    }
}
