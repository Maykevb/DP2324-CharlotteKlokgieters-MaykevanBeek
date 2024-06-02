using Sudoku.models.visitors;

namespace Sudoku.models.SudokuComponent
{
    public class SudokuCell : iSudokuComponent
    {
        private int value;
        private bool isFixed;
        private int[] notes;
		private int? block;
        private bool isCorrect;

        public SudokuCell(int value, bool isFixed, int notes)
        {
            this.value = value;
            this.isFixed = isFixed;
            this.notes = new int[notes];
            this.isCorrect = true;
		}

        public SudokuCell(int value, bool isFixed, int notes, int block)
        {
            this.value = value;
            this.isFixed = isFixed; 
            this.notes = new int[notes];
			this.block = block;
			this.isCorrect = true;
		}

		public void Accept(iBoardVisitor visitor)
        {
            visitor.VisitCell(this, true);
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

        public bool IsCorrect
        {
            get { return isCorrect; }
            set { isCorrect = value; }
        }
	}
}
