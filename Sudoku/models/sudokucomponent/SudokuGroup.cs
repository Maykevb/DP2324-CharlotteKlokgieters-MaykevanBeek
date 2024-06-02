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

        public bool AddNormalNote(int row, int col, int value)
        {
            int boardSize = (int)Math.Sqrt(components.Count);
            int index = (row - 1) * boardSize + (col - 1);
            SudokuCell cell = (SudokuCell)components[index];

            return AddNote(row,col, value, cell);
        }

        public bool AddNote(int row, int col, int value, SudokuCell cell)
        {
            if (cell.IsFixed)
            {
                Console.WriteLine($"The cell at row {row} and col {col} is not changable.");
                return false;
            }

            if (cell.Notes.Contains(value))
            {
                int noteIndex = Array.IndexOf(cell.Notes, value);
                cell.Notes[noteIndex] = 0;
                gameController.displayBoard(type);

                return true;
            }

            cell.Notes[value - 1] = value;

            gameController.displayBoard(type);
            return true;
        }

        public bool FillNormalCell(int row, int col, int value)
        {
            int boardSize = (int)Math.Sqrt(components.Count);
            int index = (row - 1) * boardSize + (col - 1);

            return FillCell(index, row, col, components, value);
        }

        public bool HandleSamuraiCell(int row, int col, int value, bool note)
        {
            List<int> groupIndices = GetGroupIndex(row, col);

            foreach (int groupIndex in groupIndices)
            {
                if (note)
                {
                    Console.WriteLine("hoi");
                    if (groupIndex == 1 || groupIndex == 4)
                    {
                        col += 12; // Rechter subgroepen
                    }

                    if (groupIndex == 3 || groupIndex == 4)
                    {
                        row += 12; // Onderste subgroepen
                    }

                    if (groupIndex == 2)
                    {
                        col += 6;
                        row += 6;// Middelste subgroep
                    }
                }

                SudokuGroup group = (SudokuGroup)components[groupIndex];
                int rowWithinGroup = row;
                int colWithinGroup = col;

                if (groupIndex == 1 || groupIndex == 4)
                {
                    colWithinGroup -= 12; // Rechter subgroepen
                }
                
                if (groupIndex == 3 || groupIndex == 4)
                {
                    rowWithinGroup -= 12; // Onderste subgroepen
                }
                
                if (groupIndex == 2)
                {
                    colWithinGroup -= 6;
                    rowWithinGroup -= 6;// Middelste subgroep
                }

                int cellIndex = (rowWithinGroup - 1) * 9 + (colWithinGroup - 1);
                if(!note)
                {
                    return FillCell(cellIndex, row, col, group.components, value);
                } 

                Console.WriteLine(row + " " + col);
                return AddNote(row, col, value, (SudokuCell)group.components[cellIndex]);
            }

            return false;
        }

        private bool FillCell(int index, int row, int col, List<iSudokuComponent> cells, int value)
        {
            if (index < 0 || index >= cells.Count)
            {
                Console.WriteLine($"Cell at row {row} and column {col} does not exist.");
                return false;
            }

            if (cells[index].IsFixed)
            {
                Console.WriteLine($"Cell at row {row} and column {col} is not changable.");
                return false;
            }

            cells[index].Value = (cells[index].Value == value) ? 0 : value;
            gameController.displayBoard(type);

            return true;
        }

        private List<int> GetGroupIndex(int row, int col)
        {
            List<int> groupIndices = new List<int>();

            if (row <= 9 && col <= 9)
            {
                groupIndices.Add(0); // Bovenste linker subgroep
            }
            else if (row <= 9 && col > 12)
            {
                groupIndices.Add(1); // Bovenste rechter subgroep
            }
            else if (row > 12 && col <= 9)
            {
                groupIndices.Add(3); // Onderste linker subgroep
            }
            else if (row > 12 && col > 12)
            {
                groupIndices.Add(4); // Onderste rechter subgroep
            }
            else
            {
                groupIndices.Add(2); // Centrale subgroep
            }

            if ((row > 9 && row <= 12) || (col > 9 && col <= 12))
            {
                groupIndices.Add(2); // Centrale subgroep
            }

            return groupIndices;
        }

        // Visitors
        public void Accept(iBoardVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void SwitchState(iBoardState newState)
        {
            this.State = newState;
        }

        // Getters & Setters
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
        }
    }
}
