using Sudoku.models.visitors;

namespace Sudoku.models.SudokuComponent
{
    public class SudokuCell : iSudokuComponent
    {
        private int value;
        private bool isFixed;
        private int[] notes;
        // TODO: make notes shorter if 4x4 or 6x6
        /*private int[] notes = new int[9];*/ //TODO from master ??
		private int? block;
        private int correctValue = 0;
        private int[] autoSolveNotes;

        public SudokuCell(int value, bool isFixed, int notes)
        {
            this.value = value;
            this.isFixed = isFixed;
            this.notes = new int[notes];
            this.autoSolveNotes = new int[notes];

			SetCorrectValue(value, isFixed);
            FillAutoNotes(notes, this.correctValue);
		}

        public SudokuCell(int value, bool isFixed, int notes, int block)
        {
            this.value = value;
            this.isFixed = isFixed;
            this.block = block;
			this.autoSolveNotes = new int[notes];

			SetCorrectValue(value, isFixed);
            FillAutoNotes(notes, this.correctValue);
		}

        private void SetCorrectValue(int value, bool isFixed)
        {
			if (isFixed)
			{
				this.correctValue = value;
			}
		}

        private void FillAutoNotes(int notes, int correctValue)
        {
            if (correctValue == 0)
            {
                for (int i = 0; i < notes; i++)
                {
                    this.autoSolveNotes[i] = (i + 1);
			    }

                return;
            }

            this.autoSolveNotes[0] = correctValue;
        }

        public void Accept(iBoardVisitor visitor)
        {
            visitor.VisitCell(this);
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

        public int CorrectValue
        {
            get { return correctValue; }
            set { correctValue = value; }
        }

		public int[] AutoSolveNotes 
        { 
            get { return autoSolveNotes; }
            set { autoSolveNotes = value; }
        }
	}
}
