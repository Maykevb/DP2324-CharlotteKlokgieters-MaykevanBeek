using Sudoku.models.SudokuComponent;

namespace Sudoku.renderers
{
    public interface iBoardRenderer : ICloneable
    {
        public void DrawBoard(SudokuGroup board, int squareLength, int squareHeight)
        {

        }
    }
}
