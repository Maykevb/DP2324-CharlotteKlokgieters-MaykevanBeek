using Sudoku.models.states;
using Sudoku.models.visitors;
using System;
using System.ComponentModel.Design;

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
            visitor.Visit(this);
        }

        public void SwitchState(iBoardState newState)
        {
            this.State = newState;
        }

        public Boolean FillSamuraiCell(int row, int col, int value)
        {
            List<int> groupIndices = GetGroupIndex(row, col);

            foreach (int groupIndex in groupIndices)
            {
                SudokuGroup group = (SudokuGroup)components[groupIndex];
                int rowWithinGroup = row;
                int colWithinGroup = col;

                if (groupIndex == 1 || groupIndex == 4)
                {
                    colWithinGroup -= 12; // Aanpassen voor de rechter subgroepen
                }
                
                if (groupIndex == 3 || groupIndex == 4)
                {
                    rowWithinGroup -= 12; // Aanpassen voor de onderste subgroepen
                }
                
                if (groupIndex == 2)
                {
                    colWithinGroup -= 6;
                    rowWithinGroup -= 6;// Aanpassen voor de middelste subgroep
                }
                

                int cellIndex = (rowWithinGroup - 1) * 9 + (colWithinGroup - 1);
                if (cellIndex >= 0 && cellIndex < group.components.Count)
                {
                    if (group.components[cellIndex].IsFixed)
                    {
                        Console.WriteLine($"Cell at row {row} and column {col} is not changable.");
                        return false;
                    }

                    group.components[cellIndex].Value = value;
                    gameController.ClearConsole();
                    gameController.displayBoard(type);
                    return true;
                }
                else
                {
                    Console.WriteLine($"Cell at row {row} and column {col} does not exist.");
                    return false;
                }
            }

            return false;
        }

        private List<int> GetGroupIndex(int row, int col)
        {
            List<int> groupIndices = new List<int>();

            if (row <= 9 && col <= 9)
            {
                // Bovenste linker subgroep
                groupIndices.Add(0);
            }
            else if (row <= 9 && col > 12)
            {
                // Bovenste rechter subgroep
                groupIndices.Add(1);
            }
            else if (row > 12 && col <= 9)
            {
                // Onderste linker subgroep
                groupIndices.Add(3);
            }
            else if (row > 12 && col > 12)
            {
                // Onderste rechter subgroep
                groupIndices.Add(4);
            }
            else
            {
                // Centrale subgroep
                groupIndices.Add(2);
            }

            if ((row > 9 && row <= 12) || (col > 9 && col <= 12))
            {
                // Centrale subgroep
                groupIndices.Add(2);
            }

            return groupIndices;
        }

        public Boolean FillCell(int row, int col, int value)
        {
            int boardSize = (int)Math.Sqrt(components.Count);
            int index = (row - 1) * boardSize + (col - 1);

            if (index >= 0 && index < components.Count)
            {
                if (components[index].IsFixed)
                {
                    Console.WriteLine($"Cell at row {row} and column {col} is not changable.");
                    return false;
                } 

                components[index].Value = value;
                gameController.ClearConsole();
                gameController.displayBoard(type);
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
        }
    }
}
