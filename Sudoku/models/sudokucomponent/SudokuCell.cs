using Sudoku.models.visitors;

namespace Sudoku.models.SudokuComponent
{
    public class SudokuCell : iSudokuComponent
    {
        private int value;
        private bool isFixed;
        private int[] notes = new int[9];
        private int? block;

        public SudokuCell(int value, bool isFixed)
        {
            this.value = value;
            this.isFixed = isFixed;
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
