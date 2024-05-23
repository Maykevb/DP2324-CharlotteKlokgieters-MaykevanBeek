using Sudoku.models.BoardComponent;

namespace Sudoku.renderers
{
    public interface iBoardRenderer : ICloneable
    {
        public void DrawBoard(SudokuBoard board, int squareLength, int squareHeight)
        {

        }

		public void clearBoard()
        {
            //TODO
            Console.Clear();
        }
    }
}
