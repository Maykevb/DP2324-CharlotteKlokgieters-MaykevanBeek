using Sudoku.models.states;
using Sudoku.models.visitors;

namespace Sudoku.models.SudokuComponent
{
	public class SudokuGroup : iSudokuComponent
    {
        private static readonly int CORRECTION_SUBGROUPS = 12;
        private static readonly int CORRECTION_SUBGROUPS_MIDDLE = 6;
        private static readonly int INDEX_SUBGROUPS_LEFT = 9;
        private static readonly int INDEX_SUBGROUPS_MIDDLE_LEFT = 7;
        private static readonly int INDEX_SUBGROUPS_MIDDLE_RIGHT = 15;

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
                    colWithinGroup -= CORRECTION_SUBGROUPS; // Right subgroups
                }
                
                if (groupIndex == 3 || groupIndex == 4)
                {
                    rowWithinGroup -= CORRECTION_SUBGROUPS; // Lower subgroups
                }
                
                if (groupIndex == 2)
                {
                    colWithinGroup -= CORRECTION_SUBGROUPS_MIDDLE;
                    rowWithinGroup -= CORRECTION_SUBGROUPS_MIDDLE; // Middle subgroups
                }

                int cellIndex = (rowWithinGroup - 1) * 9 + (colWithinGroup - 1);
                if (!note)
                {
                    if (!FillCell(cellIndex, row, col, group.components, value))
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

            gameController.DisplayBoard(type);
            return true;
        }

        public bool AddSamuraiNote(int row, int col, int value, bool note, int group)
        {
            if (group == 1 || group == 4)
            {
                col += CORRECTION_SUBGROUPS; // Right subgroups
            }

            if (group == 3 || group == 4)
            {
                row += CORRECTION_SUBGROUPS; // Lower subgroups
            }

            if (group == 2)
            {
                col += CORRECTION_SUBGROUPS_MIDDLE;
                row += CORRECTION_SUBGROUPS_MIDDLE; // Middle subgroups
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

            if (row <= INDEX_SUBGROUPS_LEFT && col <= INDEX_SUBGROUPS_LEFT)
            {
                groupIndices.Add(0); // Upper left subgroup
            }
            else if (row <= INDEX_SUBGROUPS_LEFT && col > CORRECTION_SUBGROUPS)
            {
                groupIndices.Add(1); // Upper right subgroup
            }
            else if (row > CORRECTION_SUBGROUPS && col <= INDEX_SUBGROUPS_LEFT)
            {
                groupIndices.Add(3); // Lower left subgroup
            }
            else if (row > CORRECTION_SUBGROUPS && col > CORRECTION_SUBGROUPS)
            {
                groupIndices.Add(4); // Lower right subgroup
            }
            else
            {
                groupIndices.Add(2); // Middle subgroup
            }

            if ((row >= INDEX_SUBGROUPS_MIDDLE_LEFT && row <= INDEX_SUBGROUPS_MIDDLE_RIGHT) || (col >= INDEX_SUBGROUPS_MIDDLE_LEFT && col <= INDEX_SUBGROUPS_MIDDLE_RIGHT))
            {
                groupIndices.Add(2); // Middle subgroup
            }

            return groupIndices;
        }

        // Visitors
		public void Accept(iBoardVisitor visitor, SudokuGroup board, int boardIndex, int celIndex, SudokuGroup fullBoard)
		{
            if (board.type == SudokuType.JIGSAW && visitor is SquareVisitor)
            {
                visitor.VisitJigsaw(board);
                return;
			}
            visitor.VisitBoard(this, boardIndex, fullBoard);
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
