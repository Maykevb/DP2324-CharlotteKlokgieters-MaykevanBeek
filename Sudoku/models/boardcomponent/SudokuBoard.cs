using Sudoku.models.states;
using Sudoku.models.visitors;

namespace Sudoku.models.BoardComponent
{
    public class SudokuBoard : iBoardComponent
    {
        private iBoardState state;
        private List<SudokuCell> cells = new List<SudokuCell>();

        public SudokuBoard()
        {
            this.state = new DefinitiveState();
        }

        public void Accept(iBoardVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void SwitchState(iBoardState newState)
        {
            this.State = newState;
        }

        public void NextState()
        {
            this.State = this.state.goNext();
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
