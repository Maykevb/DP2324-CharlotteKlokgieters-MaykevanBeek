using Sudoku.models.visitors;

namespace Sudoku.models.SudokuComponent
{
    public class SudokuCell : iSudokuComponent
    {
        private int value;
        private bool isFixed;
        // TODO: make notes shorter if 4x4 or 6x6
        private int[] notes = new int[9];
        private int? block;

        public SudokuCell(int value, bool isFixed)
        {
            this.value = value;
            this.isFixed = isFixed;

            if(isFixed)
            {
                notes[value - 1] = value;
            }
        }

        public SudokuCell(int value, bool isFixed, int block)
        {
            this.value = value;
            this.isFixed = isFixed;
            this.block = block;
        }

        public void Accept(iBoardVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public bool IsFixed
        {
            get { return isFixed; }
            set { isFixed = value; }
        }

        public int[] Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public int? Block
        {
            get { return block; }
            set { block = value; }
        }
    }
}
