using Sudoku.models.SudokuComponent;
using Sudoku.renderers;

namespace Sudoku.models.states
{
    public interface iBoardState
    {
        public void PrintState();

        public void DoAction(SudokuGroup board, GameController controller);

        public void DisplayBoard(iBoardRenderer renderer, SudokuGroup board, int length, int height);
    }
}
