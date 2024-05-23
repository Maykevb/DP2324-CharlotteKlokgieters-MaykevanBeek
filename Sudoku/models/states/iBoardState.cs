using Sudoku.models.BoardComponent;

namespace Sudoku.models.states
{
    public interface iBoardState
    {
        public void PrintState();

        public void DoAction(SudokuBoard board);
    }
}
