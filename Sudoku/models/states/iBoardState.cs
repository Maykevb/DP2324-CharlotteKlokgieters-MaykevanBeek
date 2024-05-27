using Sudoku.models.SudokuComponent;

namespace Sudoku.models.states
{
    public interface iBoardState
    {
        public void PrintState();

        public void DoAction(SudokuGroup board);
    }
}
