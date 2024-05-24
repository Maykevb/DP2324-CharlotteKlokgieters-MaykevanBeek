using Sudoku.models.states;
using Sudoku.models.visitors;

namespace Sudoku.models.BoardComponent
{
    public class SudokuGroup : iBoardComponent
    {
        private iBoardState state;
        private List<iBoardComponent> components = new List<iBoardComponent>();

        public SudokuGroup()
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

        public List<iBoardComponent> Components
        {
            get { return components; }
            set { this.components = value; }
        }

        public iBoardState State
        {
            get { return state; }
            set { this.state = value; }
        }
    }
}
