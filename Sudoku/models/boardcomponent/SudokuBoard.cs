using Sudoku.models.states;
using Sudoku.models.visitors;
using System.Drawing;

namespace Sudoku.models.BoardComponent
{
    public class SudokuBoard : iBoardComponent
    {
        private iBoardState state;
        private List<SudokuCell> cells = new List<SudokuCell>();
        private GameController gameController;
        private SudokuType type;

        public SudokuBoard(GameController gameController, SudokuType type)
        {
            this.state = new DefinitiveState();
            this.gameController = gameController;
            this.type = type;
        }

        public void Accept(iBoardVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void SwitchState(iBoardState newState)
        {
            this.State = newState;
        }

        public Boolean FillCell(int row, int col, int value)
        {
            int boardSize = (int)Math.Sqrt(cells.Count);
            int index = (row - 1) * boardSize + (col - 1);

            if (index >= 0 && index < cells.Count)
            {
                Console.WriteLine(cells[index].Value);
                if (cells[index].IsFixed)
                {
                    Console.WriteLine($"Cell at row {row} and column {col} is not changable.");
                    return false;
                } 

                cells[index].Value = value;
                gameController.displayBoard(type);
                return true;
            }
            else
            {
                Console.WriteLine($"Cell at row {row} and column {col} does not exist.");
                return false;
            }
        }

        public List<SudokuCell> Cells
        {
            get { return cells; }
            set { this.cells = value; }
        }

        public iBoardState State
        {
            get { return state; }
            set { this.state = value; }
        }
    }
}
