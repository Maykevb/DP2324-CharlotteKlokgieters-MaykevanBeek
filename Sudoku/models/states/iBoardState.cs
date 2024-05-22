using Sudoku.models.BoardComponent;

namespace Sudoku.models.states
{
    public interface iBoardState
    {
        public void Handle();
        iBoardState goNext();

        public void doAction(SudokuBoard board);
    }
}
