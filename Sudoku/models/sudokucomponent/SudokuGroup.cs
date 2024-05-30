using Sudoku.models.states;
using Sudoku.models.visitors;

namespace Sudoku.models.SudokuComponent
{
	public class SudokuGroup : iSudokuComponent
    {
        private iBoardState state;
        private List<iSudokuComponent> components = new List<iSudokuComponent>();
        private GameController gameController;
        private SudokuType type;

        public SudokuGroup(GameController gameController, SudokuType type)
        {
            this.state = new DefinitiveState();
            this.gameController = gameController;
            this.type = type;
        }

        public void Accept(iBoardVisitor visitor)
        {
            visitor.VisitBoard(this);
        }

        public void SwitchState(iBoardState newState)
        {
            this.State = newState;
        }

        public Boolean FillCell(int row, int col, int value)
        {
            int boardSize = (int)Math.Sqrt(components.Count);
            int index = (row - 1) * boardSize + (col - 1);

            if (index >= 0 && index < components.Count)
            {
                Console.WriteLine(components[index].Value);
                if (components[index].IsFixed)
                {
                    Console.WriteLine($"Cell at row {row} and column {col} is not changable.");
                    return false;
                } 

                components[index].Value = value;
                gameController.DisplayBoard(type);
                return true;
            }
            else
            {
                Console.WriteLine($"Cell at row {row} and column {col} does not exist.");
                return false;
            }
        }

        public List<iSudokuComponent> Components
        {
            get { return components; }
            set { this.components = value; }
        }

        public iBoardState State
        {
            get { return state; }
            set { this.state = value; }
        }

        public SudokuType Type
        {
            get { return type; }
            set {  type = value; }
        }
    }
}
