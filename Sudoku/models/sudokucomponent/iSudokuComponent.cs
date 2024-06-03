using Sudoku.models.visitors;

namespace Sudoku.models.SudokuComponent
{
	public interface iSudokuComponent
    {
        void Accept(iBoardVisitor visitor);

		public int Value
		{
			get { return 0; }
			set { }
		}

		public int? Block
		{
			get { return 0; }
			set { }
		}

		public bool IsFixed
		{
			get { return true; }
			set { }
		}

		public List<iSudokuComponent> Components
		{
			get { return new List<iSudokuComponent>(); }
		}
	}
}
