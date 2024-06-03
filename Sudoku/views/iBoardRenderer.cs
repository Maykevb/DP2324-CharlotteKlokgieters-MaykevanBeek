using Sudoku.models.SudokuComponent;

namespace Sudoku.renderers
{
    public interface iBoardRenderer : ICloneable
    {
        public void DrawBoard(SudokuGroup board, int squareLength, int squareHeight) { }

        public void DrawNotes(SudokuGroup board, int sQUARE_4X41, int sQUARE_4X42) { }
    }
}
