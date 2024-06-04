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
            AddNote(row,col, value, cell);

            gameController.DisplayBoard(type);

            return true;
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
                gameController.DisplayBoard(type);

                return true;
            }

            cell.Notes[value - 1] = value;

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
                if (!note)
                {
                    if(!FillCell(cellIndex, row, col, group.components, value))
                    {
                        return false;
                    }
                } else
                {
                    if (!AddNote(rowWithinGroup, colWithinGroup, value, (SudokuCell)group.components[cellIndex]))
                    {
                        return false;
                    }
                }                
            }

            if (note)
            {
                gameController.DisplayBoard(type);
            }

            return true;
        }

        public bool AddSamuraiNote(int row, int col, int value, bool note, int group)
        {
            if (group == 1 || group == 4)
            {
                col += 12; // Rechter subgroepen
            }

            if (group == 3 || group == 4)
            {
                row += 12; // Onderste subgroepen
            }

            if (group == 2)
            {
                col += 6;
                row += 6;// Middelste subgroep
            }

            return HandleSamuraiCell(row, col, value, note);
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
            gameController.DisplayBoard(type);

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

            if ((row >= 7 && row <= 15) || (col >= 7 && col <= 15))
            {
                groupIndices.Add(2); // Centrale subgroep
            }

            return groupIndices;
        }

        // Visitors
		public void Accept(iBoardVisitor visitor, SudokuGroup board, int boardIndex, int celIndex, SudokuGroup fullBoard)
		{
            if (board.type == SudokuType.JIGSAW)
            {
                visitor.VisitJigsaw(board);
                return;
			}
            visitor.VisitBoard(this, boardIndex, board);
        }

        // State
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
